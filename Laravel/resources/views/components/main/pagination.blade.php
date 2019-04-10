<!--게시글-->
<div class="title_area2">
    <h2 class="title">
        <strong>게시글</strong>
    </h2>
</div>
@foreach($boards as $board)
<div class="sub_photo" id="photo_gallery">
    <ul>
        <li>
            <p class="thmb">
                <a href="{{route('board', $board->id)}}">
                    @if(isset($board->files[0]))
                    <video width="100%">
                        <source src="{{$board->files[0]->path.$board->files[0]->name}}#t=0.1" type="video/mp4">
                        Your browser does not support HTML5 video.
                    </video>
                    @else
                    <div id="playButton_box">    
                    <img src="{{asset('images/pic/free.png')}}" class="picture"/>
                    </div>
                    @endif
                </a>
            </p>
            <div class="view_like" id="view_like_{{{$board->id}}}">
                @include('components.heart', ['board' => $board])
            </div>
            <a href="{{route('board', $board->id)}}">
                <h4 id="title_text">
                    <strong>
                        {{$board->title}}
                    </strong>
                </h4>
            </a>
            <p class="writer">{{$board->user->name}}</p>
        </li>
    </ul>
</div>
@endforeach

<!--페이지네이션-->
<center>
    <div class="pagination">
        @for($i=$page_link_first; $i<=$page_link_last; $i++)
            <li onclick="pagination({{$i}})" style="cursor:pointer">{{$i}}</li>
        @endfor
    </div>
</center>