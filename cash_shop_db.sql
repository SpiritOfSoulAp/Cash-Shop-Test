-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.42 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for cash_shop_db
CREATE DATABASE IF NOT EXISTS `cash_shop_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `cash_shop_db`;

-- Dumping structure for table cash_shop_db.inventory_items
CREATE TABLE IF NOT EXISTS `inventory_items` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `user_id` bigint unsigned DEFAULT NULL,
  `shop_item_id` bigint unsigned DEFAULT NULL,
  `acquired_at` datetime(3) DEFAULT NULL,
  `expire_at` datetime(3) DEFAULT NULL,
  `created_at` datetime(3) DEFAULT NULL,
  `updated_at` datetime(3) DEFAULT NULL,
  `deleted_at` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_inventory_items_user_id` (`user_id`),
  KEY `idx_inventory_items_shop_item_id` (`shop_item_id`),
  KEY `idx_inventory_items_deleted_at` (`deleted_at`),
  CONSTRAINT `fk_inventory_items_shop_item` FOREIGN KEY (`shop_item_id`) REFERENCES `shop_items` (`id`),
  CONSTRAINT `fk_users_inventory_items` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table cash_shop_db.inventory_items: ~0 rows (approximately)

-- Dumping structure for table cash_shop_db.purchase_logs
CREATE TABLE IF NOT EXISTS `purchase_logs` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `user_id` bigint unsigned DEFAULT NULL,
  `shop_item_id` bigint unsigned DEFAULT NULL,
  `duration_key` varchar(10) DEFAULT NULL,
  `price_paid` bigint DEFAULT NULL,
  `cash_before` bigint DEFAULT NULL,
  `cash_after` bigint DEFAULT NULL,
  `request_id` varchar(64) DEFAULT NULL,
  `purchased_at` datetime(3) DEFAULT NULL,
  `created_at` datetime(3) DEFAULT NULL,
  `updated_at` datetime(3) DEFAULT NULL,
  `deleted_at` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_purchase_logs_user_id` (`user_id`),
  KEY `idx_purchase_logs_shop_item_id` (`shop_item_id`),
  KEY `idx_purchase_logs_request_id` (`request_id`),
  KEY `idx_purchase_logs_deleted_at` (`deleted_at`),
  CONSTRAINT `fk_purchase_logs_shop_item` FOREIGN KEY (`shop_item_id`) REFERENCES `shop_items` (`id`),
  CONSTRAINT `fk_users_purchase_logs` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table cash_shop_db.purchase_logs: ~0 rows (approximately)

-- Dumping structure for table cash_shop_db.shop_items
CREATE TABLE IF NOT EXISTS `shop_items` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `code` varchar(50) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `type` enum('PERMANENT','RENTAL') DEFAULT NULL,
  `base_price` bigint DEFAULT NULL,
  `created_at` datetime(3) DEFAULT NULL,
  `updated_at` datetime(3) DEFAULT NULL,
  `deleted_at` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idx_shop_items_code` (`code`),
  KEY `idx_shop_items_deleted_at` (`deleted_at`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table cash_shop_db.shop_items: ~2 rows (approximately)
INSERT INTO `shop_items` (`id`, `code`, `name`, `type`, `base_price`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 'ITEM_A', 'Item A', 'PERMANENT', 1000, '2025-12-27 12:21:58.000', '2025-12-27 12:21:59.000', NULL),
	(2, 'ITEM_B', 'Item B', 'RENTAL', NULL, '2025-12-27 12:21:58.000', '2025-12-27 12:21:59.000', NULL);

-- Dumping structure for table cash_shop_db.shop_item_durations
CREATE TABLE IF NOT EXISTS `shop_item_durations` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `shop_item_id` bigint unsigned DEFAULT NULL,
  `duration_key` varchar(10) DEFAULT NULL,
  `seconds` bigint DEFAULT NULL,
  `price` bigint DEFAULT NULL,
  `created_at` datetime(3) DEFAULT NULL,
  `updated_at` datetime(3) DEFAULT NULL,
  `deleted_at` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_shop_item_durations_shop_item_id` (`shop_item_id`),
  KEY `idx_shop_item_durations_deleted_at` (`deleted_at`),
  CONSTRAINT `fk_shop_items_durations` FOREIGN KEY (`shop_item_id`) REFERENCES `shop_items` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table cash_shop_db.shop_item_durations: ~5 rows (approximately)
INSERT INTO `shop_item_durations` (`id`, `shop_item_id`, `duration_key`, `seconds`, `price`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 2, '1h', 3600, 100, '2025-12-27 12:26:48.000', '2025-12-27 12:26:52.000', NULL),
	(2, 2, '6h', 21600, 500, '2025-12-27 12:26:49.000', '2025-12-27 12:26:53.000', NULL),
	(3, 2, '1d', 86400, 1000, '2025-12-27 12:26:50.000', '2025-12-27 12:26:54.000', NULL),
	(4, 2, '3d', 259200, 2000, '2025-12-27 12:26:51.000', '2025-12-27 12:26:55.000', NULL),
	(6, 2, '7d', 604800, 4000, '2025-12-27 12:26:51.000', '2025-12-27 12:26:56.000', NULL);

-- Dumping structure for table cash_shop_db.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `cash` bigint NOT NULL,
  `created_at` datetime(3) DEFAULT NULL,
  `updated_at` datetime(3) DEFAULT NULL,
  `deleted_at` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_users_deleted_at` (`deleted_at`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table cash_shop_db.users: ~1 rows (approximately)
INSERT INTO `users` (`id`, `cash`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 10000, '2025-12-27 11:39:05.000', '2025-12-29 02:06:39.151', NULL);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
