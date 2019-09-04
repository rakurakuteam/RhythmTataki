<!--리스트-->
{{-- <div class="list_box"> --}}
    {{-- <p id="list"><b>{{__('messages.video_list')}}</b></p> --}}
    <div class="col-md-12 row">
        <center>
            <div class="col-md-6">
                @foreach ($files as $file)
                <div class="radio_box">
                    <label class="radio list">{{$file->song}}<p id="date">{{$file->created_at}}</p>
                        <input type="checkbox" name="song[]" value="{{$file->id}}">
                        <span class="checkround"></span>
                    </label>
                </div>
                @endforeach
            </div>
        </center>
    </div>
{{-- </div> --}}