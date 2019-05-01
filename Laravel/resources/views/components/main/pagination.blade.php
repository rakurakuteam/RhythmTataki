<!--게시글-->
<div class="title_area2">
    <h2 class="title">
        <strong>게시글</strong>
    </h2>
</div>
@foreach($boards as $board)
<div class="sub_photo">
    <ul>
        <li>
            <p class="thmb">
                <a href="{{route('board.show', $board->id)}}">
                    @if(isset($board->files[0]))
                    <video class="picture">
                        <source src="{{$board->files[0]->path.$board->files[0]->name}}#t=0.1" type="video/mp4">
                        Your browser does not support HTML5 video.
                    </video>
                    @else
                    <div class="playButton_box">    
                    <img src="{{asset('images/pic/free.png')}}" class="picture"/>
                    </div>
                    @endif
                </a>
            </p>
            <div class="view_like_{{{$board->id}}}" id="view_like_{{{$board->id}}}">
                @include('components.heart', ['board' => $board])
            </div>
            <a href="{{route('board.show', $board->id)}}">
                <h4 class="title_text">
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
    <nav class="pagination pagination_type">
        <ol class="pagination__list">
            <li class="pagination__group pagination__control_prev" onclick="pagination({{$current_page-1}})">
                prev
            </li>
            @for($i=$page_link_first; $i<=$page_link_last; $i++)
                @if($i == $current_page)
                <li class="pagination__group" onclick="pagination({{$i}})">
                    <span class="pagination__item pagination__item_active">{{$i}}</span>
                </li>
                @else
                <li class="pagination__group pagination__item" onclick="pagination({{$i}})">
                    {{$i}}
                </li>
                @endif
            @endfor
            <li class="pagination__group pagination__control_next" onclick="pagination({{$current_page+1}})">
                next
            </li>
        </ol>
    </nav>
</center>