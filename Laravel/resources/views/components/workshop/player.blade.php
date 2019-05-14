<div class="body_box">
    <div class="top_box">
        <div class="wave_box">
            <div id="waveform"></div>
        </div>
        <div class="under_box">
            <div id="music_btn_box">
                <button id="play_btn">PLAY</button>
                <button id="pause_btn">STOP</button>
                
                <input type="range" id="slider" class="slider" value="0" min="0" max="1000"/>
            </div>
            <div id="timer" class="timer">
                <div class="timer_box">
                    <div id="current_time">0:0</div>
                    <div id="icon">&nbsp&nbsp/&nbsp&nbsp</div>
                    <div id="play_time"></div>
                </div>
                <div class="cutter_box">
                    <form action="{{route('workshop.cutter')}}" method="POST">
                        @csrf
                        <input type="text" id="temporary_sound" name="temporary_sound" style="display:none" value="{{$url}}">
                        <input type="text" id="start_sec" name="start_sec">부터
                        <input type="text" id="end_sec" name="end_sec">까지
                        <input type="text" id="len" name="len" style="display:none">
                        <input type="text" id="clip_name" name="clip_name" style="width:150px;" placeholder="이름">
                        <input type="submit" id="cut_btn" value="자르기">
                        <input type="button" id="remove_btn" value="지우기">
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>