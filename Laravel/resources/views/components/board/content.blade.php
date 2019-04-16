<contents>
    <center>
        <div class="posts" id="posts">
            <div class="title_area" id="title_area">
                <a href="{{route('userPage', $board->user_id)}}">
                    <img src="{{asset('images/pic/profile.png')}}" id="profile"/>
                </a>
                <h1 id="title">{{$board->title}}</h1>
            </div>
            <div class="sub_photo" id="photo_gallery">
                <p class="thmb">
                    @if($video)
                    <video width="90%" controls controlsList="nodownload" preload="metadata">
                            <source src="{{$video}}#t=0.1" type="video/mp4">
                            Your browser does not support HTML5 video.
                    </video>
                    @else
                    <img src="{{asset('images/pic/free.png')}}"></div>
                    <div id="playButton_box">
                        <img src="{{asset('images/pic/playButton.png')}}" id="play_button"/>
                    </div>
                    @endif
                </p>
                <div class="view_like" id="view_like_{{$board->id}}">
                    @include('components.heart')
                </div>
                <div class="write_box">
                    <p>{{$board->content}}</p>
                </div>
            </div>
        </div>
    </center>
</contents>