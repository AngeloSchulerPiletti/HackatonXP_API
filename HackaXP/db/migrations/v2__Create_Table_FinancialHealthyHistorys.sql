CREATE TABLE `financialhealthyhistorys` (
	`id` INT(5) NOT NULL AUTO_INCREMENT,
	`consult_date` DATETIME NOT NULL,
	`costumer_id` VARCHAR(200) NOT NULL,
	`financial_behavior_score` INT(4) NOT NULL,
	`financial_freedom_score` INT(4) NOT NULL,
	`financial_knowledge_score` INT(4) NOT NULL,
	`financial_security_score` INT(4) NOT NULL,
	`index_score` INT(4) NOT NULL,
	PRIMARY KEY (`id`)
)
ENGINE=InnoDB DEFAULT CHARSET=latin1;