public class DFS
    {
        static public bool DFSOn = false; 
        static public bool solving = false;

        static private bool trackBack = false;

        private static Stack stackDFS = new Stack(); 
        private static Stack stackMoves = new Stack();

        private static ArrayList xVisitedList = new ArrayList(); 
        private static ArrayList yVisitedList = new ArrayList();

        public static Double[][] xTempPuzzle = new Double[3][];
        public static Double[][] yTempPuzzle = new Double[3][];

        static public int getNumberOfMovesMade()
        {
            return stackMoves.Count;
        }
        static public void initializeArray()
        {
            for (int i = 0; i < 3; i++)
            {
                xTempPuzzle[i] = new Double[3];
                yTempPuzzle[i] = new Double[3];
            }

            xTempPuzzle[0][0] = PuzzleData.xPuzzle[0][0];
            xTempPuzzle[0][1] = PuzzleData.xPuzzle[0][1];
            xTempPuzzle[0][2] = PuzzleData.xPuzzle[0][2];
            xTempPuzzle[1][0] = PuzzleData.xPuzzle[1][0];
            xTempPuzzle[1][1] = PuzzleData.xPuzzle[1][1];
            xTempPuzzle[1][2] = PuzzleData.xPuzzle[1][2];
            xTempPuzzle[2][0] = PuzzleData.xPuzzle[2][0];
            xTempPuzzle[2][1] = PuzzleData.xPuzzle[2][1];
            xTempPuzzle[2][2] = PuzzleData.xPuzzle[2][2];

            yTempPuzzle[0][0] = PuzzleData.yPuzzle[0][0];
            yTempPuzzle[0][1] = PuzzleData.yPuzzle[0][1];
            yTempPuzzle[0][2] = PuzzleData.yPuzzle[0][2];
            yTempPuzzle[1][0] = PuzzleData.yPuzzle[1][0];
            yTempPuzzle[1][1] = PuzzleData.yPuzzle[1][1];
            yTempPuzzle[1][2] = PuzzleData.yPuzzle[1][2];
            yTempPuzzle[2][0] = PuzzleData.yPuzzle[2][0];
            yTempPuzzle[2][1] = PuzzleData.yPuzzle[2][1];
            yTempPuzzle[2][2] = PuzzleData.yPuzzle[2][2];
        }
        static public void resetAll()
        {
            xVisitedList.Clear();
            yVisitedList.Clear();
            stackDFS.Clear();
            stackMoves.Clear();
            DFSOn = true;
            solving = false;
        }
        static public bool solvePuzzle()
        {
            if (DFSOn) { initializeArray(); DFSOn = false; }

            if (PuzzleData.isFinalState(DFS.xTempPuzzle, DFS.yTempPuzzle)) { return true; }

            addNextMove();
            movePuzzle();

            return false;
        }
        static private bool isValidMove(int direction)
        {
            if (direction == 0) //check if sliding left is possible
            {
                if (DFS.xTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 1) //check if sliding up is possible
            {
                if (DFS.yTempPuzzle[2][2] >= 100) { return true; }
            }
            else if (direction == 2)//check if sliding right is possible
            {
                if (DFS.xTempPuzzle[2][2] < 200) { return true; }
            }
            else if (direction == 3) //check if sliding down is possible
            {
                if (DFS.yTempPuzzle[2][2] < 200) { return true; }
            }

            return false;
        }
        static private bool isVisitedState()
        {
            for (int cnt = xVisitedList.Count - 1; cnt > -1; cnt--)
            {
                int sameElementCnt = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((((Double[][])xVisitedList[cnt])[i][j] == xTempPuzzle[i][j]) &&
                            ((Double[][])yVisitedList[cnt])[i][j] == yTempPuzzle[i][j])
                        {
                            sameElementCnt++;
                        }
                    }
                }

                if (sameElementCnt == 9) { return true; }
            }
            return false;
        }
        static private void addNextMove()
        {
            bool moveAdded = false;

            if (isValidMove(0))
            {
                PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(0);
                    moveAdded = true;
                }
            }

            if (isValidMove(1))
            {
                PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(1);
                    moveAdded = true;
                }
            }

            if (isValidMove(2))
            {
                PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(2);
                    moveAdded = true;
                }
            }

            if (isValidMove(3))
            {
                PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
                if (isVisitedState())
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                }
                else
                {
                    PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
                    stackDFS.Push(3);
                    moveAdded = true;
                }
            }
            if (!moveAdded)
            {
                int lastMove = (int)stackMoves.Pop();
                if (lastMove == 0) { lastMove = 2; }
                else if (lastMove == 1) { lastMove = 3; }
                else if (lastMove == 2) { lastMove = 0; }
                else if (lastMove == 3) { lastMove = 1; }

                stackDFS.Push(lastMove);
                trackBack = true;
            }
        }
        static private void movePuzzle()
        {
            int direction = (int)stackDFS.Pop();

            if (direction == 0) 
            {
                PuzzleData.moveLeft(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 1) 
            {
                PuzzleData.moveUp(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 2) 
            {
                PuzzleData.moveRight(xTempPuzzle, yTempPuzzle);
            }
            else if (direction == 3) 
            {
                PuzzleData.moveDown(xTempPuzzle, yTempPuzzle);
            }
            if (!trackBack) { addMove(direction); }
            addVisitedList();

            trackBack = false;
        }
        static private void addVisitedList()
        {
            Double[][] xTemp = new Double[3][];
            xTemp[0] = new Double[3];
            xTemp[1] = new Double[3];
            xTemp[2] = new Double[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    xTemp[i][j] = xTempPuzzle[i][j];
                }
            }
            xVisitedList.Add(xTemp);

            Double[][] yTemp = new Double[3][];
            yTemp[0] = new Double[3];
            yTemp[1] = new Double[3];
            yTemp[2] = new Double[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    yTemp[i][j] = yTempPuzzle[i][j];
                }
            }
            yVisitedList.Add(yTemp);
        }
        static private void addMove(int direction)
        {
            stackMoves.Push(direction);
        }
    }