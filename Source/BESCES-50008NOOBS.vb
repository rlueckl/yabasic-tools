'*************************'
' Die Noobie Baeren Bande '
'*************************'
'  Cheap Snake Knock-Off  '
' Made by: DRuNKeN_MaSTeR '
'*************************'

win_width = 640
win_height = 512

open window win_width,win_height

 plx = 395 : rem Starting X position
 ply = 268 : rem Starting Y position
plsp =   2 : rem Initial speed

plg  =  55   : rem Player color G
plb  = 145   : rem Player color B
plc$ = "add" : rem Start pulsing

prev_speed = 0

timer = -1 : rem Draw inital header
foodtimer = -1
foodanim = 100

direction = 32 : rem Initial D
last_dir = 32

dim help$(9)
help$(1) = "Move:      D-Pad"
help$(2) = "Restart:  Select"
help$(3) = " "
help$(4) = "Food:  10 points"
help$(5) = "Wall: instadeath"
help$(6) = " "
help$(7) = "Speed:"
help$(8) = "+50% every 50pts"

help_h = 70 : rem Help text starting height

high = 0
score = 0

label game
    gosub buffer

    gosub draw_header
    gosub draw_playarea
    gosub draw_help
    gosub draw_scores

    gosub place_food
    gosub draw_player

    gosub eat_food
    gosub update_speed

    if (plx+10 > 620) or (plx < 180) or (ply+10 > 492) or (ply < 52) then
        gosub reset
    endif

    gosub player_move
goto game

label buffer
    setdrawbuf draw
    draw = 1 - draw
    setdispbuf draw
    clear window
return

label draw_header
    if (timer < 0) then
        timer = 150 : rem Should be about 3 sec. with PAL/50fps
        text_r = 112+ran(128)
        text_g = 112+ran(128)
        text_b = 112+ran(128)
    endif

    setrgb 1,text_r,text_g,text_b
    text 400,30,"Die Noobie Baeren Bande","cc"
    timer = timer - 1
return

label draw_playarea
    setrgb 1,192,192,192
    rem Inner window / play area
    rectangle win_width-460,win_height-460,win_width-20,win_height-20
    rem Outer window / for design
    rectangle win_width-462,win_height-462,win_width-18,win_height-18
return

label draw_help
    setrgb 1,255,255,255
    for cnt = 1 to 8
        text 5,help_h,help$(cnt),"lc"
        help_h = help_h + 22
        if (cnt = 8) then
            help_h = 70
        endif
    next cnt
return

label draw_scores
    if (score > high) then
        high = score
    endif

    setrgb 1,0,75,165
    text 90,300,"Your score","cc"
    text 90,330,str$(score),"cc"
    text 90,380,"Highscore","cc"
    text 90,410,str$(high),"cc"
return

label place_food
    if (foodtimer < 0) then
        rem Food is placed after eaten or moved every 3-6 seconds
        foodtimer = 150 + ran(150)

        fx = 185 + ran(430)
        fy = 57 + ran(430)

        fr = 20 + ran(235)
        fg = 20 + ran(235)
        fb = 20 + ran(235)
    endif

    setrgb 1,fr,fg,fb
    rem fill rectangle fx,fy to fx+8,fy+8
    fill circle fx,fy,4
    foodtimer = foodtimer - 1
return

label draw_player
    if (plg > 85) then
        plc$ = "remove"
    endif
    if (plg < 55) then
        plc$ = "add"
    endif

    if (plc$ = "remove") then
        plg = plg - 1
        plb = plb - 1
    endif
    if (plc$ = "add") then
        plg = plg + 1
        plb = plb + 1
    endif

    setrgb 1,0,plg,plb
    fill rectangle plx,ply to plx+10,ply+10

    if (foodanim < 20) then
        setrgb 1,fr,fg,fb
        rectangle plx-foodanim,ply-foodanim to plx+10+foodanim,ply+10+foodanim
        foodanim = foodanim + 3
    endif
return

label player_move
    if (direction = 16) then
        ply=ply-plsp : rem UP
        last_dir = 16
    elsif (direction = 64) then
        ply=ply+plsp : rem DOWN
        last_dir = 64
    elsif (direction = 128) then
        plx=plx-plsp : rem LEFT
        last_dir = 128
    elsif (direction = 32) then
        plx=plx+plsp : rem RIGHT
        last_dir = 32
    else
        if (last_dir = 16) then
            ply=ply-plsp : rem UP
        elsif (last_dir = 64) then
            ply=ply+plsp : rem DOWN
        elsif (last_dir = 128) then
            plx=plx-plsp : rem LEFT
        elsif (last_dir = 32) then
            plx=plx+plsp : rem RIGHT
        endif
    endif

    direction = peek("port1")
return

label eat_food
    rem Is the player on a food piece?
    rem Add score and "reset food"

    if (plx <= fx-4) and (fx-4 <= plx+10) then
        if (ply <= fy-4) and (fy-4 <= ply+10) then
            score = score + 10
            foodtimer = -1
            foodanim = 0
        endif
    endif

    if (plx <= fx+4) and (fx+4 <= plx+10) then
        if (ply <= fy+4) and (fy+4 <= ply+10) then
            score = score + 10
            foodtimer = -1
            foodanim = 0
        endif
    endif
return

label update_speed
    rem Add 0.5 speed for every 50 points

    tmp_speed = int(score / 50)

    if (tmp_speed > prev_speed) then
        plsp = plsp + 0.5
        prev_speed = prev_speed + 1
    endif
return

label reset
    rem If the player gets out of the play area
    rem -> show "pause menu" and reset variables
    repeat
        gosub buffer

        gosub draw_header
        gosub draw_playarea
        gosub draw_help
        gosub draw_scores

        setrgb 1,155,5,5
        text 400,252,"YOU DIED","cc"
        text 400,282,"PRESS \"START\" TO RESTART","cc"
    until (peek("port1") = 8)
    
    direction = 32
    last_dir = 32
    timer = -1
    foodtimer = -1

    score = 0

    plx = 395
    ply = 268

    plsp = 2
    prev_speed = 0
return
