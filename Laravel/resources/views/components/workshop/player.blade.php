<div class="row">
    <div class="col-md-12">
        <div class="col-md-12">
            <input type="text" id="temporary_sound" style="display:none" value="{{$url}}">
            <div id="waveform"></div>
        </div>
        <div id="controller">
            <button id="sound_play">play</button>
            <button id="sound_pause">stop</button> 
        </div>
        <div id="timer" class="col-md-12">
            <div class="row">
                <div id="current_time" class="col-md-11">0:0</div>
                <div id="play_time" class="col-md-1"></div>
            </div>
            <div class="row">
                <form action="{{route('workshop.cutter')}}" method="POST">
                    @csrf
                    <input type="text" id="start_sec" name="start_sec" style="width:60px;">부터
                    <input type="text" id="end_sec" name="end_sec" style="width:60px;">까지
                    <input type="text" id="clip_name" name="clip_name" style="width:150px;" placeholder="이름">
                    <input type="submit" value="자르기">
                    <input type="button" id="region_remove" value="지우기">
                </form>
            </div>
        </div>
    </div>
</div>