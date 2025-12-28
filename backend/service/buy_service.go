package service

import (
	"cash_shop/db"
	"cash_shop/models"
	"errors"
	"time"

	"gorm.io/gorm"
)

type BuyInput struct {
	UserID      uint64
	ItemID      uint64
	DurationKey string
	RequestID   string
}

type BuyResult struct {
	CashBefore  int
	CashAfter   int
	InventoryID uint64
	ExpireAt    *time.Time
}

func Buy(input BuyInput) (*BuyResult, error) {

	var result *BuyResult

	err := db.DB.Transaction(func(tx *gorm.DB) error {

		// Load user
		var user models.User
		if err := tx.First(&user, input.UserID).Error; err != nil {
			return errors.New("USER_NOT_FOUND")
		}

		// Load item
		var item models.ShopItem
		if err := tx.First(&item, input.ItemID).Error; err != nil {
			return errors.New("ITEM_NOT_FOUND")
		}

		price := item.BasePrice
		var expireAt *time.Time

		// Rental logic
		if item.Type == models.ShopItemRental {
			var duration models.ShopItemDuration
			if err := tx.
				Where("shop_item_id = ? AND seconds = ?", item.ID, input.DurationKey).
				First(&duration).Error; err != nil {
				return errors.New("DURATION_NOT_FOUND")
			}

			price = duration.Price
			t := time.Now().Add(time.Duration(duration.Seconds) * time.Second)
			expireAt = &t

		}

		// Check cash
		if user.Cash < price {
			return errors.New("INSUFFICIENT_CASH")
		}

		cashBefore := user.Cash
		user.Cash -= price

		if err := tx.Save(&user).Error; err != nil {
			return err
		}

		// Inventory
		inventory := models.InventoryItem{
			UserID:     user.ID,
			ShopItemID: item.ID,
			AcquiredAt: time.Now(),
			ExpireAt:   expireAt,
		}

		if err := tx.Create(&inventory).Error; err != nil {
			return err
		}

		// Purchase log
		log := models.PurchaseLog{
			UserID:      user.ID,
			ShopItemID:  item.ID,
			DurationKey: input.DurationKey,
			PricePaid:   price,
			CashBefore:  cashBefore,
			CashAfter:   user.Cash,
			RequestID:   input.RequestID,
			PurchasedAt: time.Now(),
		}

		if err := tx.Create(&log).Error; err != nil {
			return err
		}

		result = &BuyResult{
			CashBefore:  cashBefore,
			CashAfter:   user.Cash,
			InventoryID: inventory.ID,
			ExpireAt:    expireAt,
		}

		return nil
	})

	if err != nil {
		return nil, err
	}

	return result, nil
}
