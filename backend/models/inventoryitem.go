package models

import (
	"time"

	"gorm.io/gorm"
)

type InventoryItem struct {
	ID         uint64 `gorm:"primaryKey"`
	UserID     uint64 `gorm:"index"`
	ShopItemID uint64 `gorm:"index"`

	AcquiredAt time.Time
	ExpireAt   *time.Time 

	User     User     `gorm:"foreignKey:UserID"`
	ShopItem ShopItem `gorm:"foreignKey:ShopItemID"`

	gorm.Model
}
