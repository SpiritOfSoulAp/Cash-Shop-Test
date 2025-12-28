package controller

import (
	"cash_shop/models"
	"cash_shop/response"

	"github.com/gin-gonic/gin"
)

func GetUser(ctx *gin.Context) {
	id := ctx.Param("id")

	var u models.User
	if err := u.GetUserByID(id); err != nil {
		response.Error(ctx, 404, "USER_NOT_FOUND", "User not found")
		return
	}

	response.OK(ctx, gin.H{
		"id":   u.ID,
		"cash": u.Cash,
	})
}
