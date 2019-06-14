<contents>
    <center>
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
                <p class="thmb">
                    @if(isset($video))
                    <video width="90%" controls controlsList="nodownload" preload="metadata">
                            <source src="{{$video}}#t=0.1" type="video/mp4">
                            Your browser does not support HTML5 video.
                    </video>
                    @else
                    <img src="{{asset('images/pic/free.png')}}">
                    @endif
                </p>
                <div>
                    @foreach($board->files as $list)
                        <h3>{{$list->song}}</h3>
                    @endforeach
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
    </center>
</contents>
