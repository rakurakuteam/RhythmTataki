 <img src="{{asset('images/pic/search.png')}}" id="search" />
    <center>
    <div class="middle">
        <div class="rank">
            <div class="ranking_post">
                @foreach($rankings as $ranking)
                <div class="post_under">
                    <a href="{{route('board.show', ['id' => $ranking->id])}}">
                        <img src="{{asset('images/pic/thumb_1.png')}}" id="thmb" />
                    </a>
                    <div class="text_area_1">
                        <img src="{{asset('images/pic/view.png')}}" id="view" />
                        <p id="view_t">{{$ranking->hits}}</p>
                        <img src="{{asset('images/pic/like_1.png')}}" id="like" />
                        <p id="like_t">{{$ranking->total_heart}}</p>
                        @if(\Auth::check())
                        <img src="{{asset('images/pic/download.png')}}" class="download" onclick="download({{$ranking->id}})" style="cursor:pointer"/>
                        @else
                        <img src="{{asset('images/pic/download.png')}}" class="download"/>
                        @endif
                    </div>

                    <div class="text_area_2">
                        <p id="title">{{$ranking->title}}</p>
                    </div>
                </div>
                @endforeach
            </div>
            <div class="notice">
                <img src="{{asset('images/pic/sort.png')}}" id="sort" />
                @foreach($boards as $board)
                <div class="r_post_under">
                    <a href="{{route('board.show', ['id' => $board->id])}}">
                        <img src="{{asset('images/pic/thumb_2.png')}}" id="thmb" />
                    </a>
                    <div class="text_area_1">
                        <img src="{{asset('images/pic/view.png')}}" id="view" />
                        <p id="view_t">{{$board->hits}}</p>
                        <img src="{{asset('images/pic/like_1.png')}}" id="like" />
                        <p id="like_t">{{$board->total_heart}}</p>
                        @if(\Auth::check())
                        <img src="{{asset('images/pic/download.png')}}" class="download" onclick="download({{$board->id}})" style="cursor:pointer"/>
                        @else
                        <img src="{{asset('images/pic/download.png')}}" class="download"/>
                        @endif
                    </div>
                    <div class="text_area_2">
                        <p id="title">{{$board->title}}</p>
                    </div>
                </div>
                @endforeach
            </div>
        </div>
    </div>
</center>