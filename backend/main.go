package main

import (
	"cash_shop/db"
	"cash_shop/models"
	"fmt"
	"log"

	"github.com/gin-gonic/contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
)

func main() {
	if err := godotenv.Load(); err != nil {
		log.Fatal("Error Loading File")
	}
	fmt.Println("Env Loading File")

	// Connect DB
	db.ConnectDB()
	AutoMigrate()

	// set cors all port
	corsConfig := cors.DefaultConfig()

	// Creatr Gin gonic
	r := gin.Default()

	r.Use(cors.New(corsConfig))
	serveRoutes(r)

	r.Run(":4444")

}

func AutoMigrate() {
	err := db.DB.AutoMigrate(
		&models.User{},
		&models.ShopItem{},
		&models.ShopItemDuration{},
		&models.InventoryItem{},
		&models.PurchaseLog{},
	)
	if err != nil {
		log.Fatal(err)
	}
}
