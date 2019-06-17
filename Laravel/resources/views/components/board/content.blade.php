<contents>
    <center>
        @if(isset($board->files[1])) {{-- 패키지 --}}
            <div class="package_posts">
                <div class="title_area" id="title_area">
                    <a href="{{route('userPage', $board->user_id)}}">
                        <img src="{{asset('images/pic/profile.png')}}" id="profile"/>
                    </a>
                    <div class="title_box">
                        <h1 id="title">{{$board->title}}</h1>
                    </div>
                </div>
                <div class="sub_photo">
                    <div id='package_content'>
                        <div id='video'>
                            @if(isset($video))
                                @include('components.board.video', ['video' => $video])
                            @else
                                <img src="{{asset('images/pic/free.png')}}">
                            @endif
                        </div>
                        <div></div>
                        {{-- <div id='package'>
                            <div id="package_list">
                                @foreach($board->files as $list)
                                    <div class="video_list">
                                        <div class="video_title_box">
                                            <h3 class="video_title" onclick="videoChange({{$list->pivot->file_id}})">{{$list->song}}</h3>
                                        </div>
                                    </div>
                                @endforeach
                            </div>
                        </div> --}}
                        <ul {{-- id="package_list" --}} class="list-group {{-- list-group-flush --}}">
                            @foreach($board->files as $list)
                            <div class="video_list">
                                @if($list->song == $first_name)
                                    <a class="list-group-item list-group-item-action list-group-item-primary video_list"
                                    onclick="videoChange({{$list->pivot->file_id}}, event)">
                                        {{$list->song}}
                                    </a>
                                @else
                                    <a class="list-group-item list-group-item-action video_list"
                                    onclick="videoChange({{$list->pivot->file_id}}, event)">
                                        {{$list->song}}
                                    </a>
                                @endif
                            </div>
                            @endforeach
                        </ul>
                        <div class="like_box">
                        <div class="view_like_{{$board->id}}" id="view_like_{{$board->id}}">
                            @include('components.heart', ['board' => $board])
                        </div>
                        <div></div>
                        <div></div>
                        <div class="package_write_box">
                            <p id="write">{{$board->content}}</p>
                        </div>
                    </div>
                </div>
            </div>
        @endif
        @if(!isset($board->files[1])) {{-- 싱글 --}}
            <div class="posts" id="posts">
                <div class="title_area" id="title_area">
                    <a href="{{route('userPage', $board->user_id)}}">
                        <img src="{{asset('images/pic/profile.png')}}" id="profile"/>
                    </a>
                    <div class="title_box">
                        <h1 id="title">{{$board->title}}</h1>
                    </div>
                </div>
                <div class="sub_photo" id="photo_gallery">
                    <div id='content'>
                        <div id='video'>
                            @if(isset($video))
                                @include('components.board.video', ['video' => $video])
                            @else
                                <img src="{{asset('images/pic/free.png')}}">
                            @endif
                        </div>
                        <div class="like_box">
                            <div class="view_like_{{$board->id}}" id="view_like_{{$board->id}}">
                                @include('components.heart', ['board' => $board])
                            </div>
                        </div>
                        <div class="write_box">
                            <p id="write">{{$board->content}}</p>
                        </div>
                    </div>
                </div>  
            </div>
        @endif
    </center>
</contents>
