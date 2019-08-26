<div class="body_box">
    <div class="top_box">
        <div class="wave_box">
            <div id="waveform"></div>
        </div>
        <div class="under_box">
            <input type="range" id="slider" class="slider" value="0" min="0" max="100"/>
            <div class="timer_box">
                <div id="current_time">0:0</div>
                <div id="play_time"></div>
            </div>
            <div id="control_box">
                <img src="{{asset('images/pic/start_btn.png')}}" alt="start" id="play_btn">
                <img src="{{asset('images/pic/stop_btn.png')}}" alt="stop" id="pause_btn">
            </div>
            <div class="cutter_box">
                <form action="{{route('workshop.cutter')}}" method="POST" id="flex_box">
                    @csrf
                    <div id="input_box">   
                        <input type="text" id="temporary_sound" name="temporary_sound" style="display:none"
                            value="{{$url}}">
                        <input type="text" id="start_sec" name="start_sec">{{__('messages.from')}}
                        <input type="text" id="end_sec" name="end_sec">{{__('messages.to')}}
                        <input type="text" id="len" name="len" style="display:none">
                        <input type="text" id="clip_name" name="clip_name" style="width:150px;"
                            placeholder="{{__('messages.name')}}">
                    </div>
                    <div id="buuton_box">
                        <button id="cut_btn">
                            <img src="{{asset('images/pic/save_btn.png')}}" alt="save" >
                        </button>
                        <button type="button" id="remove_btn">
                            <img src="{{asset('images/pic/remove_btn.png')}}" alt="remove" id="remove_img">
                        </button>
                    </div>
                </form>
            </div>
        </div>
        {{-- </div> --}}
    </div>
</div>