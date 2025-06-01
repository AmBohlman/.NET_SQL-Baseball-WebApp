CREATE DATABASE  IF NOT EXISTS `bsbl_lg_app` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `bsbl_lg_app`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: bsbl_lg_app
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `gamestbl`
--

DROP TABLE IF EXISTS `gamestbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gamestbl` (
  `gameID` int NOT NULL AUTO_INCREMENT,
  `awayTeamID` int NOT NULL,
  `homeTeamID` int NOT NULL,
  `homeScore` int NOT NULL DEFAULT '0',
  `awayScore` int NOT NULL DEFAULT '0',
  `datePlayed` date NOT NULL,
  `gameLocation` varchar(150) NOT NULL,
  PRIMARY KEY (`gameID`),
  KEY `fk_game_awayteam` (`homeTeamID`),
  CONSTRAINT `fk_game_awayteam` FOREIGN KEY (`homeTeamID`) REFERENCES `teaminfotbl` (`teamID`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_game_hometeam` FOREIGN KEY (`homeTeamID`) REFERENCES `teaminfotbl` (`teamID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gamestbl`
--

LOCK TABLES `gamestbl` WRITE;
/*!40000 ALTER TABLE `gamestbl` DISABLE KEYS */;
INSERT INTO `gamestbl` VALUES (1,2,1,2,1,'2025-04-03','186 Birch St'),(2,1,3,3,0,'2025-04-05','521 Pine Ln'),(3,6,1,4,2,'2025-04-07','186 Birch St'),(4,1,7,1,3,'2025-04-09','123 Elm Way'),(5,3,2,1,0,'2025-04-10','734 Oak Ave'),(6,2,4,2,5,'2025-04-12','908 Maple Dr'),(7,7,2,3,1,'2025-04-14','734 Oak Ave'),(8,4,3,1,2,'2025-04-15','521 Pine Ln'),(9,3,5,6,2,'2025-04-17','345 Spruce Ct'),(10,5,4,0,1,'2025-04-19','908 Maple Dr'),(11,4,6,3,2,'2025-04-21','670 Cedar Rd'),(12,6,5,2,4,'2025-04-23','345 Spruce Ct'),(13,5,7,1,0,'2025-04-25','123 Elm Way'),(14,7,6,2,3,'2025-04-28','670 Cedar Rd');
/*!40000 ALTER TABLE `gamestbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hittingstatstbl`
--

DROP TABLE IF EXISTS `hittingstatstbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hittingstatstbl` (
  `playerID` int NOT NULL,
  `atBats` int NOT NULL DEFAULT '0',
  `plateAppearances` int NOT NULL DEFAULT '0',
  `walks` int NOT NULL DEFAULT '0',
  `strikeouts` int NOT NULL DEFAULT '0',
  `hitByPitch` int NOT NULL DEFAULT '0',
  `stolenBases` int NOT NULL DEFAULT '0',
  `hits` int NOT NULL DEFAULT '0',
  `doubles` int NOT NULL DEFAULT '0',
  `triples` int NOT NULL DEFAULT '0',
  `homeRuns` int NOT NULL DEFAULT '0',
  `runsBattedIn` int NOT NULL DEFAULT '0',
  `runs` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`playerID`),
  CONSTRAINT `fk_hittingstats_playerID` FOREIGN KEY (`playerID`) REFERENCES `playerinfotbl` (`playerID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hittingstatstbl`
--

LOCK TABLES `hittingstatstbl` WRITE;
/*!40000 ALTER TABLE `hittingstatstbl` DISABLE KEYS */;
INSERT INTO `hittingstatstbl` VALUES (1,4,5,1,1,0,0,1,0,0,0,0,1),(3,5,6,1,2,0,1,2,1,0,0,1,1),(4,3,4,0,1,1,0,0,0,0,0,0,0),(5,4,4,0,0,0,0,1,1,0,0,1,0),(6,5,5,0,1,0,0,2,0,0,0,1,1),(7,3,4,1,1,0,0,1,0,0,0,0,1),(8,4,5,1,0,0,1,3,1,0,1,2,2),(9,3,3,0,2,0,0,0,0,0,0,0,0),(10,4,5,1,1,0,0,1,0,0,0,1,0),(12,5,6,0,1,1,0,2,0,0,0,0,1),(13,3,3,0,1,0,0,1,0,0,0,0,0),(14,4,5,1,2,0,0,0,0,0,0,0,0),(15,4,4,0,0,0,0,2,1,0,0,1,1),(16,3,4,1,0,0,1,1,0,0,0,1,1),(17,5,5,0,2,0,0,1,0,0,0,0,0),(18,3,4,1,1,0,0,1,1,0,0,1,1),(19,4,4,0,1,0,0,1,0,0,0,0,1),(21,5,6,1,1,0,0,2,0,0,0,1,0),(22,3,3,0,0,0,0,1,1,0,0,1,1),(23,4,5,0,2,1,0,1,0,0,0,0,0),(24,4,4,0,1,0,1,2,1,0,0,2,1),(25,3,4,1,1,0,0,0,0,0,0,0,0),(26,5,5,0,2,0,0,1,0,0,0,0,1),(27,3,4,1,0,0,0,1,0,0,0,1,0),(28,4,5,1,2,0,0,1,0,0,0,0,1),(30,5,6,1,1,0,0,1,0,0,0,1,1),(31,3,3,0,1,0,0,0,0,0,0,0,0),(32,4,4,0,1,0,0,2,1,0,0,1,0),(33,4,5,0,1,1,0,1,0,0,0,0,1),(34,3,4,1,0,0,0,1,0,0,0,0,1),(35,5,5,0,0,0,1,3,0,1,0,2,1),(36,3,3,0,1,0,0,1,0,0,0,0,0),(37,4,4,0,2,0,0,0,0,0,0,0,0),(39,5,6,1,1,0,0,2,1,0,0,1,1),(40,3,4,1,1,0,0,1,0,0,0,0,1),(41,4,5,1,0,0,0,1,0,0,0,1,0),(42,4,4,0,1,0,0,1,0,0,0,0,0),(43,3,3,0,0,0,0,2,0,0,0,1,1),(44,5,6,0,1,1,0,2,1,0,0,1,1),(45,3,4,1,2,0,0,0,0,0,0,0,0),(46,4,5,1,1,0,0,1,1,0,0,1,1),(48,5,5,0,2,0,0,1,0,0,0,0,0),(49,3,4,0,0,1,0,1,0,0,0,1,0),(50,4,4,0,1,0,0,2,0,0,0,0,1),(51,4,5,1,1,0,0,1,0,0,0,0,0),(52,3,3,0,1,0,0,1,0,0,0,1,1),(53,5,6,1,0,0,1,2,1,0,0,1,1),(54,3,4,1,1,0,0,0,0,0,0,0,0),(55,4,4,0,1,0,0,1,0,0,0,0,1),(57,5,6,1,2,0,0,1,1,0,0,1,0),(58,3,4,1,0,0,0,1,0,0,0,0,1),(59,4,5,0,1,1,0,2,0,0,0,1,1),(60,4,4,0,0,0,0,1,0,0,0,1,0),(61,3,3,0,1,0,0,1,0,0,0,0,0),(62,5,5,0,1,0,0,2,1,0,0,1,1),(63,3,4,1,2,0,0,0,0,0,0,0,0);
/*!40000 ALTER TABLE `hittingstatstbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pitchingstatstbl`
--

DROP TABLE IF EXISTS `pitchingstatstbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pitchingstatstbl` (
  `playerID` int NOT NULL,
  `inningsPitched` decimal(5,1) NOT NULL DEFAULT '0.0',
  `strikes` int NOT NULL DEFAULT '0',
  `balls` int NOT NULL DEFAULT '0',
  `walks` int NOT NULL DEFAULT '0',
  `strikeouts` int NOT NULL DEFAULT '0',
  `hitsAllowed` int NOT NULL DEFAULT '0',
  `earnedRuns` int NOT NULL DEFAULT '0',
  `saves` int NOT NULL DEFAULT '0',
  `unearnedRuns` int NOT NULL DEFAULT '0',
  `chargedWins` int NOT NULL DEFAULT '0',
  `chargedLosses` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`playerID`),
  CONSTRAINT `fk_pitchingstats_playerID` FOREIGN KEY (`playerID`) REFERENCES `playerinfotbl` (`playerID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pitchingstatstbl`
--

LOCK TABLES `pitchingstatstbl` WRITE;
/*!40000 ALTER TABLE `pitchingstatstbl` DISABLE KEYS */;
INSERT INTO `pitchingstatstbl` VALUES (2,4.0,62,31,3,5,6,7,0,0,3,1),(11,4.0,58,28,2,6,5,5,0,0,3,1),(20,4.0,65,35,4,3,8,9,0,0,1,3),(29,4.0,68,38,5,4,10,10,0,0,1,3),(38,4.0,60,29,2,7,6,7,0,0,2,2),(47,4.0,70,40,6,3,11,11,0,0,2,2),(56,4.0,63,33,3,5,7,8,0,0,2,2);
/*!40000 ALTER TABLE `pitchingstatstbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playerinfotbl`
--

DROP TABLE IF EXISTS `playerinfotbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playerinfotbl` (
  `playerID` int NOT NULL AUTO_INCREMENT,
  `playerFN` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `playerLN` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DOB` date NOT NULL,
  `teamID` int NOT NULL,
  `jerseyNumber` int NOT NULL,
  `position` varchar(2) NOT NULL,
  PRIMARY KEY (`playerID`),
  KEY `fk_playerInfoTBL_teaminfo` (`teamID`),
  CONSTRAINT `fk_playerInfoTBL_teaminfo` FOREIGN KEY (`teamID`) REFERENCES `teaminfotbl` (`teamID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerinfotbl`
--

LOCK TABLES `playerinfotbl` WRITE;
/*!40000 ALTER TABLE `playerinfotbl` DISABLE KEYS */;
INSERT INTO `playerinfotbl` VALUES (1,'Zephyr','Stone','1998-03-15',1,5,'C'),(2,'Orion','Swift','1999-07-22',1,12,'P'),(3,'Lyra','Redfern','1997-11-02',1,23,'1B'),(4,'Cassian','Moon','2000-01-30',1,8,'2B'),(5,'Theron','Gale','1998-09-18',1,17,'3B'),(6,'Elara','Cloud','1999-05-09',1,2,'SS'),(7,'Rhys','Brook','1997-06-25',1,33,'LF'),(8,'Seren','Forrest','2000-04-01',1,21,'CF'),(9,'Kael','Hill','1998-12-12',1,10,'RF'),(10,'Jasper','Flint','1999-02-10',2,4,'C'),(11,'Astrid','Ironhide','1997-08-05',2,11,'P'),(12,'Finn','Blackwood','2000-06-14',2,22,'1B'),(13,'Clara','Silvermane','1998-01-20',2,7,'2B'),(14,'Hugo','Hawthorne','1999-10-03',2,16,'3B'),(15,'Dahlia','Shadowend','1997-04-28',2,1,'SS'),(16,'Ian','Star','2000-07-07',2,30,'LF'),(17,'Esme','Frost','1998-11-11',2,19,'CF'),(18,'Jack','Storm','1999-03-27',2,9,'RF'),(19,'Leo','Peak','1997-05-19',3,6,'C'),(20,'Maya','Valley','1999-09-01',3,14,'P'),(21,'Neo','Meadow','1998-07-24',3,25,'1B'),(22,'Nora','Grove','2000-02-08',3,3,'2B'),(23,'Oscar','Dell','1997-12-03',3,18,'3B'),(24,'Olive','Marsh','1999-06-11',3,20,'SS'),(25,'Paul','Field','1998-04-07',3,28,'LF'),(26,'Piper','Mountain','2000-10-26',3,13,'CF'),(27,'Quinn','Ocean','1997-01-17',3,27,'RF'),(28,'Rowan','Riverwood','1999-11-08',4,24,'C'),(29,'Stella','Stryker','1998-02-22',4,1,'P'),(30,'Sam','Blade','1997-09-14',4,15,'1B'),(31,'Thea','Rook','2000-05-03',4,26,'2B'),(32,'Tom','Wraith','1999-01-06',4,7,'3B'),(33,'Ursa','Shade','1998-08-30',4,11,'SS'),(34,'Vic','Specter','1997-03-12',4,31,'LF'),(35,'Vera','Grim','2000-12-01',4,20,'CF'),(36,'Will','Night','1999-07-19',4,4,'RF'),(37,'Xeo','Cipher','1998-06-04',5,3,'C'),(38,'Willow','Vector','1997-10-10',5,18,'P'),(39,'Yan','Matrix','2000-03-29',5,29,'1B'),(40,'Xyla','Node','1999-08-08',5,6,'2B'),(41,'Zack','Pixel','1998-05-21',5,12,'3B'),(42,'Yara','Byte','1997-02-16',5,22,'SS'),(43,'Axel','Kernel','2000-09-05',5,35,'LF'),(44,'Zoe','Script','1999-04-13',5,17,'CF'),(45,'Blaze','Hex','1998-11-25',5,5,'RF'),(46,'Caelan','Quill','1997-07-01',6,2,'C'),(47,'Briar','Talon','2000-01-23',6,16,'P'),(48,'Devon','Beak','1998-10-09',6,30,'1B'),(49,'Eamon','Hoot','1999-06-06',6,10,'2B'),(50,'Faelan','Branch','1997-03-31',6,21,'3B'),(51,'Giselle','Feather','2000-08-17',6,9,'SS'),(52,'Heath','Perch','1998-05-02',6,27,'LF'),(53,'Iris','Wisdom','1999-12-14',6,13,'CF'),(54,'Jett','Stealth','1997-09-03',6,1,'RF'),(55,'Kai','Wave','2000-04-20',7,7,'C'),(56,'Luna','Current','1998-11-05',7,19,'P'),(57,'Milo','Splash','1997-06-10',7,32,'1B'),(58,'Noel','Reef','1999-02-25',7,4,'2B'),(59,'Oren','Fin','2000-10-15',7,25,'3B'),(60,'Priya','Coral','1998-07-02',7,14,'SS'),(61,'Remy','Tide','1997-01-08',7,29,'LF'),(62,'Sorin','Aqua','1999-09-22',7,11,'CF'),(63,'Talia','Spray','2000-05-28',7,8,'RF');
/*!40000 ALTER TABLE `playerinfotbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `standingstbl`
--

DROP TABLE IF EXISTS `standingstbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `standingstbl` (
  `teamID` int NOT NULL,
  `wins` int NOT NULL DEFAULT '0',
  `losses` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`teamID`),
  CONSTRAINT `fk_standings_teaminfo` FOREIGN KEY (`teamID`) REFERENCES `teaminfotbl` (`teamID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `standingstbl`
--

LOCK TABLES `standingstbl` WRITE;
/*!40000 ALTER TABLE `standingstbl` DISABLE KEYS */;
INSERT INTO `standingstbl` VALUES (1,3,1),(2,3,1),(3,1,3),(4,1,3),(5,2,2),(6,2,2),(7,2,2);
/*!40000 ALTER TABLE `standingstbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teaminfotbl`
--

DROP TABLE IF EXISTS `teaminfotbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teaminfotbl` (
  `teamID` int NOT NULL AUTO_INCREMENT,
  `teamName` varchar(45) NOT NULL,
  `city` varchar(45) NOT NULL,
  `state` varchar(2) NOT NULL,
  `fieldAddress` varchar(150) NOT NULL,
  `coachFN` varchar(45) NOT NULL,
  `coachLN` varchar(45) NOT NULL,
  `coachPhone` varchar(25) NOT NULL,
  PRIMARY KEY (`teamID`),
  UNIQUE KEY `teamID_UNIQUE` (`teamID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teaminfotbl`
--

LOCK TABLES `teaminfotbl` WRITE;
/*!40000 ALTER TABLE `teaminfotbl` DISABLE KEYS */;
INSERT INTO `teaminfotbl` VALUES (1,'Binary','Baltimore','MD','186 Birch St','Pearl','Simmons','(991)-555-0176'),(2,'Silverbacks','Saint Louis','MO','734 Oak Ave','Jasper','Thorne','(992)-555-0123'),(3,'Malware','Omaha','NE','521 Pine Ln','Willow','Creek','(993)-555-0189'),(4,'Mantas','Minneapolis','MN','908 Maple Dr','River','Stone','(994)-555-0145'),(5,'Algorithms','Pittsburgh','PA','345 Spruce Ct','Skyler','Bloom','(995)-555-0167'),(6,'Owls','Indianapolis','IN','670 Cedar Rd','Phoenix','Hale','(996)-555-0132'),(7,'Dolphins','Hartford','CT','123 Elm Way','Sage','Forrest','(997)-555-0198');
/*!40000 ALTER TABLE `teaminfotbl` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'bsbl_lg_app'
--
/*!50003 DROP PROCEDURE IF EXISTS `displayGamesSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayGamesSTPD`()
BEGIN
    SELECT
        g.gameID,
        g.datePlayed,
        g.gameLocation,
        ht.teamName,
        ht.city,
        g.homeScore,
        at.teamName,
        at.city,
        g.awayScore
    FROM
        gamestbl g
    INNER JOIN
        teaminfotbl ht ON g.homeTeamID = ht.teamID
    INNER JOIN
        teaminfotbl at ON g.awayTeamID = at.teamID
    ORDER BY
        g.datePlayed, g.gameID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `displayHittingStatsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayHittingStatsSTPD`()
BEGIN
    SELECT
        t.teamName,
        p.playerFN,
        p.playerLN,
        hs.atBats,
        hs.plateAppearances,
        hs.hits,
        hs.runs,
        hs.walks,
        hs.stolenBases,
        hs.strikeouts,
        hs.doubles,
        hs.triples,
        hs.homeruns,
        hs.hitByPitch,

        CASE
            WHEN hs.atBats = 0 THEN 0.000
            ELSE ROUND(hs.hits / hs.atBats, 3)
        END AS AVG,

        CASE
            WHEN hs.plateAppearances = 0 THEN 0.000
            ELSE ROUND((hs.hits + hs.walks + hs.hitByPitch) / hs.plateAppearances, 3)
        END AS OBP,

        CASE
            WHEN hs.atBats = 0 THEN 0.000
            ELSE ROUND(
                (
                    (hs.hits - hs.doubles - hs.triples - hs.homeruns) +
                    (hs.doubles * 2) +
                    (hs.triples * 3) +
                    (hs.homeruns * 4)
                ) / hs.atBats, 3)
        END AS SLG,    

        ROUND(
            (CASE 
                WHEN hs.plateAppearances = 0 THEN 0.0 
                ELSE (hs.hits + hs.walks + hs.hitByPitch) / hs.plateAppearances 
            END) +
            (CASE 
                WHEN hs.atBats = 0 THEN 0.0 
                ELSE (
                        ((hs.hits - hs.doubles - hs.triples - hs.homeruns)) + 
                        (hs.doubles * 2) + 
                        (hs.triples * 3) + 
                        (hs.homeruns * 4)
                     ) / hs.atBats 
            END)
        , 3) AS OPS  
    FROM
        `playerinfotbl` p
    INNER JOIN
        `hittingstatstbl` hs ON p.playerID = hs.playerID
    INNER JOIN
        `teaminfotbl` t ON p.teamID = t.teamID
    ORDER BY
        t.teamName, p.playerLN, p.playerFN;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `displayPitchingStatsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayPitchingStatsSTPD`()
BEGIN
    SELECT
        t.teamName,
        p.playerFN,
        p.playerLN,
        ps.inningsPitched,
        ps.strikes,
        ps.balls,
        ps.walks,
        ps.strikeouts,
        ps.hitsAllowed,
        ps.earnedRuns,
        ps.saves,
        ps.unearnedRuns,
        ps.chargedWins,
        ps.chargedLosses,

        CASE
            WHEN ps.inningsPitched = 0 THEN 0.00
            ELSE ROUND((ps.earnedRuns * 9) / ps.inningsPitched, 2)
        END AS ERA,

        CASE
            WHEN ps.inningsPitched = 0 THEN 0.000
            ELSE ROUND((ps.walks + ps.hitsAllowed) / ps.inningsPitched, 3)
        END AS WHIP
    FROM
        `playerinfotbl` p
    INNER JOIN
        `pitchingstatstbl` ps ON p.playerID = ps.playerID
    INNER JOIN
        `teaminfotbl` t ON p.teamID = t.teamID
    ORDER BY
        t.teamName, p.playerLN, p.playerFN;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `displayPlayerInfoSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayPlayerInfoSTPD`(
    IN p_playerID INT
)
BEGIN
    SELECT
        pi.playerID,
        pi.playerFN,
        pi.playerLN,
        pi.DOB,
        pi.jerseyNumber,
        pi.position,
        ti.teamName,
        ti.city
    FROM
        `playerinfotbl` pi
    LEFT JOIN
        `teaminfotbl` ti ON pi.teamID = ti.teamID
    WHERE
        pi.playerID = p_playerID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `displayTeamInfoSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayTeamInfoSTPD`(
IN p_teamID INT)
BEGIN
SELECT * FROM `teaminfotbl`
WHERE `teamID` = p_teamID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `displayTeamStandingsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `displayTeamStandingsSTPD`()
BEGIN
    SELECT
    t.city,
    t.teamName,
    s.wins,
    s.losses,
    ROUND(
        CASE
            WHEN (s.wins + s.losses) = 0 THEN 0.0
            ELSE CAST(s.wins AS DECIMAL(7, 4)) / (s.wins + s.losses)
        END,
        3)
        AS winPercentage
    FROM
        `teaminfotbl` t
    INNER JOIN
        `standingstbl` s ON t.teamID = s.teamID
    ORDER BY
        winPercentage DESC, s.wins DESC;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `enterGameSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `enterGameSTPD`(
IN p_homeID INT,
IN p_awayID INT,
IN p_homeScore INT,     
IN p_awayScore INT,
IN p_date DATE,
IN p_location VARCHAR(150)
)
BEGIN
INSERT INTO `gamestbl` (
        `homeTeamID`,
        `awayTeamID`,
        `homeScore`,
        `awayScore`,
        `datePlayed`,
        `gameLocation`        
    )
    VALUES (
		p_homeID,
        p_awayID,
        p_homeScore,
        p_awayScore,
        p_date,
		p_location        
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `enterPlayerSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `enterPlayerSTPD`(
	IN p_playerFN VARCHAR(45),
    IN p_playerLN VARCHAR(45),
    IN p_dateOfBirth DATE,
    IN p_teamID INT,
    IN p_jerseyNum INT,
    IN p_position VARCHAR(2)
)
BEGIN
    INSERT INTO `playerinfotbl` (
        `playerFN`,
        `playerLN`,
        `DOB`,
        `teamID`,
        `jerseyNumber`,
        `position`
    )
    VALUES (
        p_playerFN,
        p_playerLN,
        p_dateOfBirth,
        p_teamID,
        p_jerseyNum,
        p_position
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `enterTeamSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `enterTeamSTPD`(
	IN p_teamName VARCHAR(45),
    IN p_city VARCHAR(45),
    IN p_state VARCHAR(2),
    IN p_fieldAddress VARCHAR(150),
    IN p_coachFN VARCHAR(45),
    IN p_coachLN VARCHAR(45),
    IN p_coachPhone VARCHAR(25)
)
BEGIN
    INSERT INTO `teaminfotbl` (
        `teamName`,
        `city`,
        `state`,
        `fieldAddress`,
        `coachFN`,
        `coachLN`,
        `coachPhone`
    )
    VALUES (
        p_teamName,
        p_city,
        p_state,
        p_fieldAddress,
        p_coachFN,
        p_coachLN,
        p_coachPhone
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getPlayerIDSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPlayerIDSTPD`()
BEGIN
    SELECT `playerID`, `playerFN`, `playerLN`
    FROM `playerinfotbl`
    ORDER BY `playerLN`;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getTeamIDSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getTeamIDSTPD`()
BEGIN
    SELECT `teamID`, `city`, `teamName`
    FROM `teaminfotbl`
    ORDER BY `teamName`;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `removePlayerSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `removePlayerSTPD`(
    IN p_playerID INT
)
BEGIN
    DELETE FROM `playerinfotbl`
    WHERE `playerID` = p_playerID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `updateHittingStatsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateHittingStatsSTPD`(
    IN p_playerID INT,
    IN p_ab INT,
    IN p_pa INT,
    IN p_runs INT,
    IN p_ks INT,
    IN p_bbs INT,
    IN p_hbp INT,
    IN p_sb INT,
    IN p_2b INT,
    IN p_3b INT,
    IN p_hr INT,
    IN p_rbi INT,
    IN p_hits INT 
)
BEGIN
    INSERT INTO `hittingstatstbl` (
        `playerID`, `atBats`, `plateAppearances`, `runs`, `strikeOuts`, 
        `walks`, `hitByPitch`, `stolenBases`, `doubles`, `triples`, 
        `homeRuns`, `runsBattedIn`, `hits`
    )
    VALUES (
        p_playerID, p_ab, p_pa, p_runs, p_ks, 
        p_bbs, p_hbp, p_sb, p_2b, p_3b, 
        p_hr, p_rbi, p_hits
    )
    ON DUPLICATE KEY UPDATE
        `atBats` = `atBats` + p_ab,
        `plateAppearances` = `plateAppearances` + p_pa,
        `runs` = `runs` + p_runs,
        `strikeOuts` = `strikeOuts` + p_ks,
        `walks` = `walks` + p_bbs,
        `hitByPitch` = `hitByPitch` + p_hbp,
        `stolenBases` = `stolenBases` + p_sb,
        `doubles` = `doubles` + p_2b,
        `triples` = `triples` + p_3b,
        `homeRuns` = `homeRuns` + p_hr,
        `runsBattedIn` = `runsBattedIn` + p_rbi,
        `hits` = `hits` + p_hits;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `updatePitchingStatsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `updatePitchingStatsSTPD`(
    IN p_playerID INT,
    IN p_ip DECIMAL(5,1),
    IN p_strikes INT,
    IN p_balls INT,
    IN p_bbs INT,
    IN p_ks INT,
    IN p_hitsAllowed INT,
    IN p_earnedRuns INT,
    IN p_saves INT,
    IN p_unearnedRuns INT,
    IN p_wins INT,
    IN p_losses INT
)
BEGIN
    INSERT INTO `pitchingstatstbl` (
        `playerID`, `inningsPitched`, `strikes`, `balls`, `walks`, 
        `strikeouts`, `hitsAllowed`, `earnedRuns`, `saves`, `unearnedRuns`, 
        `chargedWins`, `chargedLosses`
    )
    VALUES (
        p_playerID, p_ip, p_strikes, p_balls, p_bbs, 
        p_ks, p_hitsAllowed, p_earnedRuns, p_saves, p_unearnedRuns, 
        p_wins, p_losses
    )
    ON DUPLICATE KEY UPDATE
        `inningsPitched` = `inningsPitched` + p_ip,
        `strikes` = `strikes` + p_strikes,
        `balls` = `balls` + p_balls,
        `walks` = `walks` + p_bbs,
        `strikeouts` = `strikeouts` + p_ks,
        `hitsAllowed` = `hitsAllowed` + p_hitsAllowed,
        `earnedRuns` = `earnedRuns` + p_earnedRuns,
        `saves` = `saves` + p_saves,
        `unearnedRuns` = `unearnedRuns` + p_unearnedRuns,
        `chargedWins` = `chargedWins` + p_wins,
        `chargedLosses` = `chargedLosses` + p_losses;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `updateStandingsSTPD` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateStandingsSTPD`(
    IN p_winnerID INT,
    IN p_loserID INT
)
BEGIN
    IF p_winnerID IS NOT NULL AND p_loserID IS NOT NULL AND p_winnerID <> p_loserID THEN
        INSERT INTO `standingstbl` (`teamID`, `wins`, `losses`)
        VALUES (p_winnerID, 1, 0)
        ON DUPLICATE KEY UPDATE
            `wins` = `wins` + 1;

        INSERT INTO `standingstbl` (`teamID`, `wins`, `losses`)
        VALUES (p_loserID, 0, 1)
        ON DUPLICATE KEY UPDATE
            `losses` = `losses` + 1;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-30 22:12:40
