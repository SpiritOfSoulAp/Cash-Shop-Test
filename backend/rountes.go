package main

import (
	"cash_shop/controller"

	"github.com/gin-gonic/gin"
)

func serveRoutes(r *gin.Engine) {

	api := r.Group("/api")

	api.GET("/user/:id", controller.GetUser)
	api.GET("/shop/items", controller.GetItems)
	api.GET("/inventory", controller.GetInventory)
	api.POST("/shop/buy", controller.Buy)
}
