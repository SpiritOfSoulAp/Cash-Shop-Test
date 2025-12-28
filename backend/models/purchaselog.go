package models

import (
	"time"

	"gorm.io/gorm"
)

type PurchaseLog struct {
	ID         uint64 `gorm:"primaryKey"`
	UserID     uint64 `gorm:"index"`
	ShopItemID uint64 `gorm:"index"`

	DurationKey string `gorm:"size:10"`
	PricePaid   int
	CashBefore  int
	CashAfter   int
	RequestID   string `gorm:"size:64;index"`

	PurchasedAt time.Time

	User     User     `gorm:"foreignKey:UserID"`
	ShopItem ShopItem `gorm:"foreignKey:ShopItemID"`

	gorm.Model
}
