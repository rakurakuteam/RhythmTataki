<img src="{{asset('images/pic/view.png')}}" class="view"/>
<p class="view_text">{{$board->hits}}</p>

@if(isset($board->hearts[0]) && $board->hearts[0]->heart == true)
    <img src="{{asset('images/pic/like.png')}}" class="like" onclick="heartToggle({{$board->id}})" style="cursor:pointer"/>
@elseif(\Auth::check())
    <img src="{{asset('images/pic/like_1.png')}}" class="like" onclick="heartToggle({{$board->id}})" style="cursor:pointer"/>
@else
    <img src="{{asset('images/pic/like_1.png')}}" class="like"/>
@endif
<p class="like_text">{{$board->total_heart}}</p>

<img src="{{asset('images/pic/download.png')}}" class="download"/>