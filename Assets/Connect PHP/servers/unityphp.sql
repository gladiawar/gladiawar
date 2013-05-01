-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 15, 2011 at 01:13 PM
-- Server version: 5.5.8
-- PHP Version: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `unityphp`
--

-- --------------------------------------------------------

--
-- Table structure for table `advert`
--

CREATE TABLE IF NOT EXISTS `advert` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `url` varchar(1024) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `advert`
--


-- --------------------------------------------------------

--
-- Table structure for table `chatchannel`
--

CREATE TABLE IF NOT EXISTS `chatchannel` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `pm_channel` char(1) NOT NULL DEFAULT '0',
  `created` datetime NOT NULL,
  `uid1` int(10) unsigned NOT NULL,
  `uid2` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `chatchannel`
--


-- --------------------------------------------------------

--
-- Table structure for table `chatmessage`
--

CREATE TABLE IF NOT EXISTS `chatmessage` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `message` text NOT NULL,
  `timestamp` int(10) unsigned NOT NULL,
  `uid` int(10) unsigned NOT NULL,
  `uid2` int(10) unsigned NOT NULL,
  `cid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=32 ;

--
-- Dumping data for table `chatmessage`
--


-- --------------------------------------------------------

--
-- Table structure for table `friend`
--

CREATE TABLE IF NOT EXISTS `friend` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `uid1` int(10) unsigned NOT NULL,
  `uid2` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `friend`
--


-- --------------------------------------------------------

--
-- Table structure for table `news`
--

CREATE TABLE IF NOT EXISTS `news` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dt` datetime NOT NULL,
  `msg` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `news`
--


-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(128) NOT NULL,
  `name` varchar(16) NOT NULL,
  `name_lower` varchar(16) NOT NULL,
  `password` char(32) NOT NULL,
  `salt` varchar(8) NOT NULL,
  `created` int(10) unsigned NOT NULL,
  `updated` int(10) unsigned NOT NULL,
  `email_bmchallenge` char(1) NOT NULL DEFAULT '0',
  `shop` varchar(9) NOT NULL DEFAULT '0',
  `rating` int(9) unsigned NOT NULL DEFAULT '1500',
  `wins` int(9) unsigned NOT NULL DEFAULT '0',
  `losses` int(9) unsigned NOT NULL DEFAULT '0',
  `draws` int(9) unsigned NOT NULL DEFAULT '0',
  `quits` int(9) unsigned NOT NULL DEFAULT '0',
  `location` char(1) NOT NULL DEFAULT '0',
  `last_game_refresh` int(10) unsigned NOT NULL DEFAULT '0',
  `last_chat_refresh` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `name_lower` (`name_lower`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `name`, `name_lower`, `password`, `salt`, `created`, `updated`, `email_bmchallenge`, `shop`, `rating`, `wins`, `losses`, `draws`, `quits`, `location`, `last_game_refresh`, `last_chat_refresh`) VALUES
(3, 'demo@web.net', 'DemoUser', 'demouser', 'd22be9905f16baa2993849a86f53d246', '09477180', 1323508123, 1323947342, '1', '0', 1500, 0, 0, 0, 0, 'L', 1323947342, 1323947342);
