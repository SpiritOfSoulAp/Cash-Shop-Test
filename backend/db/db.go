package db

import (
	"fmt"
	"log"
	"os"

	"gorm.io/driver/mysql"
	"gorm.io/gorm"
	"gorm.io/gorm/logger"
)

var DB *gorm.DB

func ConnectDB() {
	dsn := os.Getenv("SQLDSN")
	db, err := gorm.Open(
		mysql.Open(dsn),
		&gorm.Config{Logger: logger.Default.LogMode(logger.Info)},
	)
	if err != nil {
		log.Fatal("Cannot Connect Database")
		return
	}
	DB = db
	fmt.Println("Database Connect Dons.")

}
