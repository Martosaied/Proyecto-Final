-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-06-2017 a las 01:54:59
-- Versión del servidor: 5.7.14
-- Versión de PHP: 5.6.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `login` (IN `email` TEXT, IN `contraseña` TEXT)  BEGIN
  SELECT * from usuarios WHERE Email=@Email and Contraseña=@Contraseña;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contenido`
--

CREATE TABLE `contenido` (
  `idContenido` int(11) NOT NULL,
  `Ruta` text NOT NULL,
  `Nombre` text NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Descripcion` longtext NOT NULL,
  `IdEscuela` int(11) DEFAULT NULL,
  `IdMateria` int(11) DEFAULT NULL,
  `Profesor` text,
  `NivelEdu` text NOT NULL,
  `TipoCont` text NOT NULL,
  `Fechadesubida` tinytext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `contenido`
--

INSERT INTO `contenido` (`idContenido`, `Ruta`, `Nombre`, `IdUsuario`, `Descripcion`, `IdEscuela`, `IdMateria`, `Profesor`, `NivelEdu`, `TipoCont`, `Fechadesubida`) VALUES
(1, 'asd', 'Matematica', 1, 'sadsad as sad sad msa s dsa dsa', 1, 1, 'asd', '1', '1', '0000-00-00 00:00:00'),
(2, 'asd', 'Matematica', 1, 'asd', 1, 1, 'asd', '2', '3', '0000-00-00 00:00:00'),
(3, '~/Uploads/20170512110640-theodorohertzl.docx', 'asdasd', 8, 'asdasd', 2, 8, 'asdasd', '1', '2', '0000-00-00 00:00:00'),
(4, '~/Uploads/20170514193719-aliot.docx', 'Lengua', 8, 'Cosas judias', 2, 8, 'Judith Faerverguer', '1', '1', '0000-00-00 00:00:00'),
(5, '~/Uploads/20170514202757-analisis de mercado.docx', 'Resumen SSI', 8, 'Alto resu,en papa', 6, 8, 'gaby', '1', '2', '0000-00-00 00:00:00'),
(13, 'asd', 'Matematica', 1, 'sadsad as sad sad msa s dsa dsa', 1, 1, 'asd', '1', '1', '0000-00-00 00:00:00'),
(14, 'asd', 'Matematica', 1, 'asd', 1, 1, 'asd', '2', '3', '0000-00-00 00:00:00'),
(15, '~/Uploads/20170512110640-theodorohertzl.docx', 'asdasd', 8, 'asdasd', 2, 8, 'asdasd', '1', '2', '0000-00-00 00:00:00'),
(16, '~/Uploads/20170514193719-aliot.docx', 'Lengua', 8, 'Cosas judias', 2, 8, 'Judith Faerverguer', '1', '1', '0000-00-00 00:00:00'),
(17, '~/Uploads/20170514202757-analisis de mercado.docx', 'Resumen SSI', 8, 'Alto resu,en papa', 6, 8, 'gaby', '1', '2', '0000-00-00 00:00:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `escuelas`
--

CREATE TABLE `escuelas` (
  `idEscuelas` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

CREATE TABLE `materias` (
  `idmaterias` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

CREATE TABLE `niveleducativo` (
  `IdNivel` int(11) NOT NULL,
  `NombreNivel` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

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

CREATE TABLE `tipodecontenido` (
  `IdTipodecont` int(11) NOT NULL,
  `NombreTipo` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

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

CREATE TABLE `usuarios` (
  `idUsuario` int(11) NOT NULL,
  `Nombre` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Apellido` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Email` varchar(45) COLLATE latin1_spanish_ci NOT NULL,
  `Contraseña` varchar(45) COLLATE latin1_spanish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;

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
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contenido`
--
ALTER TABLE `contenido`
  ADD PRIMARY KEY (`idContenido`),
  ADD KEY `IdUsuario_idx` (`IdUsuario`),
  ADD KEY `IdEscuela_idx` (`IdEscuela`),
  ADD KEY `IdMateria_idx` (`IdMateria`);

--
-- Indices de la tabla `escuelas`
--
ALTER TABLE `escuelas`
  ADD PRIMARY KEY (`idEscuelas`);

--
-- Indices de la tabla `materias`
--
ALTER TABLE `materias`
  ADD PRIMARY KEY (`idmaterias`);

--
-- Indices de la tabla `niveleducativo`
--
ALTER TABLE `niveleducativo`
  ADD PRIMARY KEY (`IdNivel`);

--
-- Indices de la tabla `tipodecontenido`
--
ALTER TABLE `tipodecontenido`
  ADD PRIMARY KEY (`IdTipodecont`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contenido`
--
ALTER TABLE `contenido`
  MODIFY `idContenido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;
--
-- AUTO_INCREMENT de la tabla `escuelas`
--
ALTER TABLE `escuelas`
  MODIFY `idEscuelas` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT de la tabla `materias`
--
ALTER TABLE `materias`
  MODIFY `idmaterias` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT de la tabla `niveleducativo`
--
ALTER TABLE `niveleducativo`
  MODIFY `IdNivel` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT de la tabla `tipodecontenido`
--
ALTER TABLE `tipodecontenido`
  MODIFY `IdTipodecont` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
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
