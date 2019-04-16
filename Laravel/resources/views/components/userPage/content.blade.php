<contents>
    <!--글박스-->
    <center>
        <!--프로필-->
        <div class="title_area" id="title_area">
            <img src="{{asset('images/pic/profile.png')}}" id="profile"/>
            <h1 id="title">{{$user->name}}</h1>
        </div>
    
        <div class="content_box">
            @foreach($boards as $board)
                <div class="sub_photo" id="photo_gallery">
                    <p class="thmb">
                        <a href="{{route('board', $board->id)}}">
                        <img src="{{asset('images/pic/free.png')}}" class="picture" href="#"></a>
                    </p>
                    <div class="view_like">
                        <img src="{{asset('images/pic/view.png')}}" class="view"/>
                        <p class="view_text">{{$board->hits}}</p>
                        <img src="{{asset('images/pic/like_1.png')}}" class="like"/>
                        <p class="like_text">{{$board->total_heart}}</p>
                        <img src="{{asset('images/pic/download.png')}}" class="download"/>
                    </div>
                    <a href="{{route('board', $board->id)}}">
                        <h4 id="title_text">
                            <strong>
                                {{$board->title}}
                            </strong>
                        </h4>
                    </a>
                    <p class="writer">{{$board->user->name}}</p>
                </div>
            @endforeach
        </div>
    </center>
</contents>