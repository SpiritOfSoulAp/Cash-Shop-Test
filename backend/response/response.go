package response

import "github.com/gin-gonic/gin"

type APIResponse struct {
	Success bool        `json:"success"`
	Code    string      `json:"code"`
	Message string      `json:"message"`
	Data    interface{} `json:"data"`
}

func OK(ctx *gin.Context, data interface{}) {
	ctx.JSON(200, APIResponse{
		Success: true,
		Code:    "OK",
		Message: "",
		Data:    data,
	})
}

func Success(ctx *gin.Context, code, message string, data interface{}) {
	ctx.JSON(200, APIResponse{
		Success: true,
		Code:    code,
		Message: message,
		Data:    data,
	})
}

func Error(ctx *gin.Context, httpStatus int, code, message string) {
	ctx.JSON(httpStatus, APIResponse{
		Success: false,
		Code:    code,
		Message: message,
		Data:    nil,
	})
}
