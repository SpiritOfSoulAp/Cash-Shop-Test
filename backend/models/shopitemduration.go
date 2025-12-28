package models

import "gorm.io/gorm"

type ShopItemDuration struct {
	ID          uint64 `gorm:"primaryKey"`
	ShopItemID  uint64 `gorm:"index"`
	DurationKey string `gorm:"size:10"`
	Seconds     int
	Price       int

	ShopItem ShopItem `gorm:"foreignKey:ShopItemID"`

	gorm.Model
}
