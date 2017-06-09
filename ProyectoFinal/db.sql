-- phpMyAdmin SQL Dump
-- version 4.5.2
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 05-06-2017 a las 17:29:34
-- Versión del servidor: 5.7.9
-- Versión de PHP: 5.6.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contenido`
--

DROP TABLE IF EXISTS `contenido`;
CREATE TABLE IF NOT EXISTS `contenido` (
  `idContenido` int(11) NOT NULL AUTO_INCREMENT,
  `Ruta` text NOT NULL,
  `Nombre` text NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Descripcion` longtext NOT NULL,
  `IdEscuela` int(11) DEFAULT NULL,
  `IdMateria` int(11) DEFAULT NULL,
  `Profesor` text,
  `NivelEdu` text NOT NULL,
  `TipoCont` text NOT NULL,
  `Fechadesubida` datetime NOT NULL,
  PRIMARY KEY (`idContenido`),
  KEY `IdUsuario_idx` (`IdUsuario`),
  KEY `IdEscuela_idx` (`IdEscuela`),
  KEY `IdMateria_idx` (`IdMateria`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `contenido`
--

INSERT INTO `contenido` (`idContenido`, `Ruta`, `Nombre`, `IdUsuario`, `Descripcion`, `IdEscuela`, `IdMateria`, `Profesor`, `NivelEdu`, `TipoCont`, `Fechadesubida`) VALUES
(1, 'asd', 'Matematica', 1, 'sadsad as sad sad msa s dsa dsa', 1, 1, 'asd', '1', '1', '0000-00-00 00:00:00'),
(2, 'asd', 'Matematica', 1, 'asd', 1, 1, 'asd', '2', '3', '0000-00-00 00:00:00'),
(3, '~/Uploads/20170512110640-theodorohertzl.docx', 'asdasd', 8, 'asdasd', 2, 8, 'asdasd', '1', '2', '0000-00-00 00:00:00'),
(4, '~/Uploads/20170514193719-aliot.docx', 'Lengua', 8, 'Cosas judias', 2, 8, 'Judith Faerverguer', '1', '1', '0000-00-00 00:00:00'),
(5, '~/Uploads/20170514202757-analisis de mercado.docx', 'Resumen SSI', 8, 'Alto resu,en papa', 6, 8, 'gaby', '1', '2', '0000-00-00 00:00:00'),
(6, '~/Uploads/20170519083542-theodorohertzl.docx', 'Tarea Hertzl', 8, 'Tarea de judia', 1, 1, 'Judith', '', '', '0000-00-00 00:00:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `escuelas`
--

DROP TABLE IF EXISTS `escuelas`;
CREATE TABLE IF NOT EXISTS `escuelas` (
  `idEscuelas` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idEscuelas`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `escuelas`
--

INSERT INTO `escuelas` (`idEscuelas`, `Nombre`) VALUES
(1, 'ORT'),
(2, 'Buber'),
(3, 'Scholem'),
(4, 'Pellegrini'),
(5, 'Nacional'),
(6, 'Weitzman');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `materias`
--

DROP TABLE IF EXISTS `materias`;
CREATE TABLE IF NOT EXISTS `materias` (
  `idmaterias` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idmaterias`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `materias`
--

INSERT INTO `materias` (`idmaterias`, `Nombre`) VALUES
(1, 'Matematica'),
(2, 'Lengua'),
(3, 'Programacion'),
(4, 'Tecnologia'),
(5, 'Sociales'),
(6, 'Etica'),
(7, 'Salud'),
(8, 'Quimica');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `niveleducativo`
--

DROP TABLE IF EXISTS `niveleducativo`;
CREATE TABLE IF NOT EXISTS `niveleducativo` (
  `IdNivel` int(11) NOT NULL AUTO_INCREMENT,
  `NombreNivel` text NOT NULL,
  PRIMARY KEY (`IdNivel`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `niveleducativo`
--

INSERT INTO `niveleducativo` (`IdNivel`, `NombreNivel`) VALUES
(1, 'Secundario'),
(2, 'Universitario'),
(3, 'Terciario');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipodecontenido`
--

DROP TABLE IF EXISTS `tipodecontenido`;
CREATE TABLE IF NOT EXISTS `tipodecontenido` (
  `IdTipodecont` int(11) NOT NULL AUTO_INCREMENT,
  `NombreTipo` text NOT NULL,
  PRIMARY KEY (`IdTipodecont`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `tipodecontenido`
--

INSERT INTO `tipodecontenido` (`IdTipodecont`, `NombreTipo`) VALUES
(1, 'Resumen'),
(2, 'Prueba'),
(3, 'Libro'),
(4, 'Apuntes'),
(5, 'Trabajos practicos'),
(6, 'Otros');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Apellido` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Email` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Contraseña` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`idUsuario`, `Nombre`, `Apellido`, `Email`, `Contraseña`) VALUES
(1, 'Martin', 'Saied', 'martusaied@gmail.com', 'cacaca'),
(3, 'Uriel', 'Bacher', 'uribacher@gmail.com', 'hola123'),
(4, 'roberto', 'roberto', 'roberto@roberto.com', 'roberto'),
(5, 'ma', 'sad', 'sad@asd', 'asd'),
(6, 'asd', 'asd', 'asd@asd', 'asdasd'),
(7, 'asdas', 'asdasd', 'asdasdqsad@s', 'asdasdasd'),
(8, 'Ezequiel', 'Weicman', 'ezeweic@gmail.com', 'Cacacaca1'),
(9, 'yo', 'ewrwer', 'adkmn@asdm.cop', 'asdasdasd1'),
(10, 'asdasd', 'adsasd', 'asdasd@aoisd.asd', 'Martinasd1');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contenido`
--
ALTER TABLE `contenido`
  ADD CONSTRAINT `IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`idUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
