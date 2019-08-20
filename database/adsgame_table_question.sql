
-- --------------------------------------------------------

--
-- Table structure for table `question`
--

CREATE TABLE `question` (
  `id` int(11) NOT NULL,
  `questionlevel` int(11) NOT NULL,
  `questiontext` varchar(500) NOT NULL,
  `answer1` varchar(200) NOT NULL,
  `answer2` varchar(200) NOT NULL,
  `answer3` varchar(200) NOT NULL,
  `answer4` varchar(200) NOT NULL,
  `rightanswer` int(11) NOT NULL,
  `category` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `question`
--

INSERT INTO `question` (`id`, `questionlevel`, `questiontext`, `answer1`, `answer2`, `answer3`, `answer4`, `rightanswer`, `category`) VALUES
(1, 1, 'Which of these best describes an array?', 'A data structure that shows a hierarchical behavio', 'Container of objects of similar types', 'Arrays are immutable once initialised', 'Array is not a data structure', 2, 'Array and Array Operations'),
(2, 1, 'How do you initialize an array in C?', 'int arr[3] = (1,2,3);', 'int arr(3) = {1,2,3};', 'int arr[3] = {1,2,3};', 'int arr(3) = (1,2,3);', 3, 'Array and Array Operations'),
(3, 1, 'How do you instantiate an array in Java?', 'int arr[] = new int(3);', 'int arr[];', 'int arr[] = new int[3];', 'int arr() = new int(3);', 3, 'Array and Array Operations'),
(4, 1, 'When does the ArrayIndexOutOfBoundsException occur?', 'Compile-time', 'Run-time', 'Not an error', 'Not an exception at all', 2, 'Array and Array Operations'),
(5, 1, ' What are the disadvantages of arrays?', 'Data structure like queue or stack cannot be imple', 'There are chances of wastage of memory space if el', ' Index value of an array can be negative', 'Elements are sequentially accessed', 2, 'Array and Array Operations'),
(6, 2, 'Process of inserting an element in stack is called ______', 'Create', 'Push', 'Evaluation', 'Pop', 2, 'Stack Operations '),
(7, 2, 'Process of removing an element from stack is called ________', 'Create', 'Push', 'Evaluation', 'Pop', 4, 'Stack Operations'),
(8, 2, 'Pushing an element into stack already having five elements and stack size of 5, then stack becomes', 'Overflow', 'Crash', 'Underflow', 'User flow', 1, 'Stack Operations '),
(9, 2, 'Entries in a stack are “ordered”. What is the meaning of this statement?', 'A collection of stacks is sortable', 'Stack entries may be compared with the ‘<‘ operation', 'The entries are stored in a linked list', 'There is a Sequential entry that is one by one', 4, 'Stack Operations '),
(10, 2, ' Which of the following applications may use a stack?', 'A parentheses balancing program', 'Tracking of local variables at run time', 'Compiler Syntax Analyzer', 'Data Transfer between two asynchronous process', 4, 'Stack Operations ');
