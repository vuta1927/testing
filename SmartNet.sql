-- MySQL Script generated by MySQL Workbench
-- Tue Mar  6 22:57:33 2018
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema SmartNet
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema SmartNet
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `SmartNet` DEFAULT CHARACTER SET utf8 ;
USE `SmartNet` ;

-- -----------------------------------------------------
-- Table `SmartNet`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`users` (
  `id` VARCHAR(36) NOT NULL,
  `username` VARCHAR(255) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `fullname` VARCHAR(555) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`roles` (
  `id` VARCHAR(36) NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `descriptions` VARCHAR(255) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`userRoles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`userRoles` (
  `id` VARCHAR(36) NOT NULL,
  `userId` VARCHAR(36) NOT NULL,
  `roleId` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `user_idx` (`userId` ASC),
  INDEX `role_idx` (`roleId` ASC),
  CONSTRAINT `user`
    FOREIGN KEY (`userId`)
    REFERENCES `SmartNet`.`users` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `role`
    FOREIGN KEY (`roleId`)
    REFERENCES `SmartNet`.`roles` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`stations`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`stations` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `address` VARCHAR(255) NOT NULL,
  `owner` VARCHAR(255) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`clients`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`clients` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `macAddress` VARCHAR(255) NOT NULL,
  `stationId` INT NOT NULL,
  `datestart` DATETIME NOT NULL,
  `descriptions` VARCHAR(500) NULL,
  PRIMARY KEY (`id`),
  INDEX `station_idx` (`stationId` ASC),
  CONSTRAINT `station`
    FOREIGN KEY (`stationId`)
    REFERENCES `SmartNet`.`stations` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`logTypes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`logTypes` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `descriptions` VARCHAR(255) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SmartNet`.`logs`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SmartNet`.`logs` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `typeId` INT NOT NULL,
  `contex` VARCHAR(500) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `log_idx` (`typeId` ASC),
  CONSTRAINT `log`
    FOREIGN KEY (`typeId`)
    REFERENCES `SmartNet`.`logTypes` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
