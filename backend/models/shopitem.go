package models

import "gorm.io/gorm"

type ShopItemType string

const (
	ShopItemPermanent ShopItemType = "PERMANENT"
	ShopItemRental    ShopItemType = "RENTAL"
)

type ShopItem struct {
	ID        uint64       `gorm:"primaryKey"`
	Code      string       `gorm:"size:50;uniqueIndex"`
	Name      string       `gorm:"size:100"`
	Type      ShopItemType `gorm:"type:enum('PERMANENT','RENTAL')"`
	BasePrice int

	Durations []ShopItemDuration

	gorm.Model
}
