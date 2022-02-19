CREATE TABLE `costumers` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(80) NOT NULL,
	`last_financial_healthy_history_id` INT(11) NOT NULL,
	`allow_test` TINYINT NOT NULL DEFAULT '0', 
	`allow_open_finance` TINYINT NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;