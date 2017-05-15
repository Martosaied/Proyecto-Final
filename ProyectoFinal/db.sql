-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db
-- ------------------------------------------------------
-- Server version	5.5.24-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `contenido`
--

DROP TABLE IF EXISTS `contenido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contenido` (
  `idContenido` int(11) NOT NULL AUTO_INCREMENT,
  `Ruta` text NOT NULL,
  `Nombre` text NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Descripcion` longtext NOT NULL,
  `IdEscuela` int(11) DEFAULT NULL,
  `IdMateria` int(11) DEFAULT NULL,
  `Profesor` text,
  PRIMARY KEY (`idContenido`),
  KEY `IdUsuario_idx` (`IdUsuario`),
  KEY `IdEscuela_idx` (`IdEscuela`),
  KEY `IdMateria_idx` (`IdMateria`),
  CONSTRAINT `IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`idUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contenido`
--

LOCK TABLES `contenido` WRITE;
/*!40000 ALTER TABLE `contenido` DISABLE KEYS */;
INSERT INTO `contenido` VALUES (1,'asd','Matematica',1,'sadsad as sad sad msa s dsa dsa',1,1,'asd'),(2,'asd','Matematica',1,'asd',1,1,'asd'),(3,'~/Uploads/20170512110640-theodorohertzl.docx','asdasd',8,'asdasd',2,8,'asdasd'),(4,'~/Uploads/20170514193719-aliot.docx','Lengua',8,'Cosas judias',2,8,'Judith Faerverguer'),(5,'~/Uploads/20170514202757-analisis de mercado.docx','Resumen SSI',8,'Alto resu,en papa',6,8,'gaby');
/*!40000 ALTER TABLE `contenido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `escuelas`
--

DROP TABLE IF EXISTS `escuelas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `escuelas` (
  `idEscuelas` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idEscuelas`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `escuelas`
--

LOCK TABLES `escuelas` WRITE;
/*!40000 ALTER TABLE `escuelas` DISABLE KEYS */;
INSERT INTO `escuelas` VALUES (1,'ORT'),(2,'Buber'),(3,'Scholem'),(4,'Pellegrini'),(5,'Nacional'),(6,'Weitzman');
/*!40000 ALTER TABLE `escuelas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materias`
--

DROP TABLE IF EXISTS `materias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `materias` (
  `idmaterias` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idmaterias`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materias`
--

LOCK TABLES `materias` WRITE;
/*!40000 ALTER TABLE `materias` DISABLE KEYS */;
INSERT INTO `materias` VALUES (1,'Matematica'),(2,'Lengua'),(3,'Programacion'),(4,'Tecnologia'),(5,'Sociales'),(6,'Etica'),(7,'Salud'),(8,'Quimica');
/*!40000 ALTER TABLE `materias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuarios` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Apellido` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Email` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Contraseña` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Martin','Saied','martusaied@gmail.com','cacaca'),(3,'Uriel','Bacher','uribacher@gmail.com','hola123'),(4,'roberto','roberto','roberto@roberto.com','roberto'),(5,'ma','sad','sad@asd','asd'),(6,'asd','asd','asd@asd','asdasd'),(7,'asdas','asdasd','asdasdqsad@s','asdasdasd'),(8,'Ezequiel','Weicman','ezeweic@gmail.com','Cacacaca1');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-14 22:47:01
