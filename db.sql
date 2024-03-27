CREATE DATABASE `csharp_todo_list`;

USE `csharp_todo_list`;

CREATE TABLE `categories` (
    `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    `parent_id` int(11) unsigned NULL,
    `name` varchar(255) NOT NULL,
    `description` TEXT NULL,
    `color` varchar(100) DEFAULT '#C3C3C3',
    `deletable` tinyint(1) NULL,
    `created` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `updated` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_Parent_Categories`
        FOREIGN KEY (`parent_id`) 
        REFERENCES `categories` (`id`)
        ON DELETE CASCADE
        ON UPDATE CASCADE
) ENGINE=InnoDB;

CREATE TABLE `tasks` (
    `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    `category_id` int(11) unsigned NOT NULL,
    `title` varchar(255) NOT NULL,
    `description` TEXT NULL,
    `start_date` TIMESTAMP NULL,
    `end_date` TIMESTAMP NULL,
    `created` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `updated` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    CONSTRAINT `FK_Task_Has_Category`
        FOREIGN KEY (`category_id`)
        REFERENCES `categories` (`id`)
        ON DELETE CASCADE 
        ON UPDATE CASCADE
) ENGINE=InnoDB;


