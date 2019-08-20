
-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(30) NOT NULL,
  `password` varchar(100) NOT NULL,
  `ranklevel` int(11) NOT NULL DEFAULT 0,
  `money` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `ranklevel`, `money`) VALUES
(1, 'Ellison', 'zwk!19951115', 1, 0),
(2, 'DXD', '19950218', 1, 1);
