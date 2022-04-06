# OBJECTIVES:
1. Make player move (DONE)
2. Make boxes interactable (DONE)
3. Make boxes switch color (DONE)

# 17/12
4. Make game end (Build and Run)
5. Make Sound (DONE)
6. Delay after 5 box found (DONE)
7. Config 2, 3, 4, 5 (DONE)
8. Display Score and Rounds 
9. Display White Screen after game end / with data
10. Test Round
11. Write puntero (DONE)

# FUTURE
12. Vector Based Pick Boxes (DONE)
13. Custom Editor Window (Set Boxes Rewrite)
14. Export data (excell) (DONE)
	13.1 Time
	13.2 Distance
	13.3 Tries (errors)
15. Add Furniture


# Interfaz:
- Iniciar (play button): calls OnStart()
- Restart (return button): Stops Run, Erases Data and calls OnStart()
- Cerrar (x button): calls Export() and then Quit()
- Test Round, Round Time, Round Number, End

# On Start():
1. Render
2. RunTestRound()
3. RunGame()


# RunTestRun():
- Toast: 	Find the green boxes as fast as you can
			Encuentre las cajas verdes lo más rapido posible
- limit 1 minute
- no rotation
- boxes interactable
- orientation and controlls


# RunGame():
- fields:
	int roundsPassed 
- Run 10 consecutive rounds for 3 minutes each
- Round() on 5 boxes / 3 minutes and roundsPassed < 10
- End if roundsPassed >= 10, stopTime()
- Toast: Congratulations, please return hardware


# RunGame :: Round():

- Grey Toast

1. Change Room Orientation:
    room is rotated: 0, 90, 180, 270 (-90) degrées.
    degree is chosen at random
    directional light is also rotated, unless skydome
    display rotation somewhere
	
2. Reset Boxes:
    MarkBoxes(degree g) calls Box.Mark() on 5 boxes depending on g
	remark based on orientation
    mark() should change boolean status

- Toast: 3, 2, 1, Start.
- startTimer(), end


# RunGame :: Frame():
	1. calls checkBoxes()
		if true: Round(), roundsPassed++


# Timer:
int <roundTime>
int <totalTime>
int <roundDistance>
int <totalDistance>
int <lastSystemTime>
f <setTime()> <getTime()> <addTime()>
  `totalTime += roundTime | roundTime = 0`
  <setTime()> <getTime()>
  <setDistance()> <getDistance()>
  <loopSystemTime()>: returns last system time recorded
  <startTimer()> <stopTimer()> 
  <startRoundTime()> <endRoundTimer()>

  `finalTime = totalTime && systemTime`

# Timer:
var <startCoord>
var <roundCoord>
var <roundDistance>

## OUTPUT
Excel Spreadsheet:
- Per Round = Distance || Time || Errors
- Final = SUM(Distance) || SUM(Time) || Errors

### Change Box model to be XZ Symmetrical DONE

# Box Object:
1. Has Base texture, red texture, green texture
2. Fields: 	boolean isMarked (false, set true) 
			boolean clicked (false on default)
			texture, fbx model, etc
3. 	isMarked() returns the isMarked field
	Mark() swithches isMarked to true
4.	hasBeenClicked () returns the clicked field
	click() swithches clicked to true
5. OnTouch() changes color and emits sound:
	if isMarked() replaces with red texture, wrong sound effect
	else replaces with green texture, correct sound effect
    calls box.click()

# Headset:
- Record distance walked and time per round.

# Controller LHR, RHR
1. OnClick(): call box.OnTouch()

# Export():
1. Calls DistanceTimeList.getList()
2. Exports list to Excel

# Room Layout:
-window-  _door_  -window-
0                     90
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
-90	                  180

## CONFIG 1
0                     90
	[] [] [X] [] []
	[X] [] [] [] []
	[] [] [] [] [X]
	[] [X] [] [] []
	[] [] [] [X] []
-90	                  180

# 0 degrees
	[01] [02] [03] [04] [05]
	[06] [07] [08] [09] [10]
	[11] [12] [13] [14] [15]
	[16] [17] [18] [19] [20]
	[21] [22] [23] [24] [25]

    Marked: 03, 06, 15, 17, 24

-window-  _door_  -window-
0                     90
	[2, 2] [] [0, 2] [] []
	[] [] [] [] []
	[] [] [0, 0] [] []
	[] [] [] [] []
	[] [] [] [] []
-90	                  180

# 90 degrees
	[05] [10] [15] [20] [25]
	[04] [09] [14] [19] [24]
	[03] [08] [13] [18] [23]
	[02] [07] [12] [17] [22]
	[01] [06] [11] [16] [21]

    Marked: 15, 04, 23, 07, 16

# 180 degrees
	[25] [24] [23] [22] [21]
	[20] [19] [18] [17] [16]
	[15] [14] [13] [12] [11]
	[10] [09] [08] [07] [06]
	[05] [04] [03] [02] [01]

    Marked: 23, 20, 11, 09, 02

# -90 degrees
	[21] [16] [11] [06] [01]
	[22] [17] [12] [07] [02]
	[23] [18] [13] [08] [03]
	[24] [19] [14] [09] [04]
	[25] [20] [15] [10] [05]

    Marked: 11, 22, 03, 19, 10

## CONFIG 2
0                     90
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
	[] [] [] [] []
-90	                  180

# 0 degrees
	[01] [02] [03] [04] [05]
	[06] [07] [08] [09] [10]
	[11] [12] [13] [14] [15]
	[16] [17] [18] [19] [20]
	[21] [22] [23] [24] [25]

    Marked: 03, 06, 15, 17, 24

# 90 degrees
	[05] [10] [15] [20] [25]
	[04] [09] [14] [19] [24]
	[03] [08] [13] [18] [23]
	[02] [07] [12] [17] [22]
	[01] [06] [11] [16] [21]

    Marked: 15, 04, 23, 07, 16

# 180 degrees
	[25] [24] [23] [22] [21]
	[20] [19] [18] [17] [16]
	[15] [14] [13] [12] [11]
	[10] [09] [08] [07] [06]
	[05] [04] [03] [02] [01]

    Marked: 23, 20, 11, 09, 02

# -90 degrees
	[21] [16] [11] [06] [01]
	[22] [17] [12] [07] [02]
	[23] [18] [13] [08] [03]
	[24] [19] [14] [09] [04]
	[25] [20] [15] [10] [05]

    Marked: 11, 22, 03, 19, 10