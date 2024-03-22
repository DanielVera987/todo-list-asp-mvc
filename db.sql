CREATE DATABASE `csharp_todo_list`;

USE `csharp_todo_list`;

CREATE TABLE `categories` (
    `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    `parent_id` int(11) unsigned NOT NULL,
    `name` varchar(255) NOT NULL,
    `description` TEXT NOT NULL,
    `color` varchar(100) DEFAULT '#C3C3C3',
    `created` TIMESTAMP DEFAULT NOW(),
    `updated` TIMESTAMP DEFAULT NOW(),
    PRIMARY KEY (`id`),
    CONSTRAINT `FK_Parent_Categories`
        FOREIGN KEY (`parent_id`) 
        REFERENCES `categories` (`id`)
        ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE `tasks` (
    `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
    `category_id` int(11) unsigned NOT NULL,
    `title` varchar(255) NOT NULL,
    `description` TEXT NOT NULL,
    `start_date` TIMESTAMP NULL,
    `end_date` TIMESTAMP NULL,
    `created` TIMESTAMP DEFAULT NOW(),
    `updated` TIMESTAMP DEFAULT NOW(),
    PRIMARY KEY (id),
    CONSTRAINT `FK_Task_Has_Category`
        FOREIGN KEY (`category_id`)
        REFERENCES `categories` (`id`)
        ON DELETE CASCADE
) ENGINE=InnoDB;


