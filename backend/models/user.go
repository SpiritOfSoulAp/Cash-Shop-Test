package models

import (
	"cash_shop/db"

	"gorm.io/gorm"
)

type User struct {
	ID   uint64 `gorm:"primaryKey"`
	Cash int    `gorm:"not null"`

	InventoryItems []InventoryItem
	PurchaseLogs   []PurchaseLog

	gorm.Model
}

func (u *User) GetUserByID(id string) error {
	return db.DB.Where("id = ?", id).Preload("InventoryItems").Preload("InventoryItems").First(&u).Error
}
