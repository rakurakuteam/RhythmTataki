<contents>
    <div class="search_box">
        <div class="search_form">
            <input type="text" placeholder="search"/>
            <a href="#">
                <button id="search_button">검색</button>
            </a>
        </div>
    </div>

    <div class="contents_1">
        <!--랭킹-->
        <div class="title_area1">
            <h2 class="title">
                <strong>랭킹</strong>
            </h2>
        </div>
        @foreach($rankings as $ranking)
        <div class="sub_photo" id="photo_gallery">
            <ul>
                <li>
                    <p class="thmb">
                        <a href="{{route('board.show', $ranking->id)}}" >
                            @if($ranking->files[0])
                            <video class="picture" controlsList="nodownload">
                                <source src="{{$ranking->files[0]->path.$ranking->files[0]->name}}#t=0.1" type="video/mp4">
                                Your browser does not support HTML5 video.
                            </video>
                            @else
                            <div id="playButton_box">
                            <img src="{{asset('images/pic/free.png')}}" class="picture"/>
                            </div>
                            @endif
                        </a>
                    </p>
                    <div class="view_like_{{$ranking->id}}" id="view_like_{{$ranking->id}}">
                        @include('components.heart', ['board' => $ranking])
                    </div>
                    <a href="{{route('board.show', $ranking->id)}}">
                        <h4 class="title_text">
                            <strong>
                                {{$ranking->title}}
                            </strong>
                        </h4>
                    </a>
                    <p class="writer">{{$ranking->user->name}}</p>
                </li>
            </ul>
        </div>
        @endforeach
    </div>
    @include('components.main.selectBox', ['current_page' => $current_page])

    <!-- 글쓰기버튼 <a class="menuLink" id="li-1" href="{{route('board.create')}}">
      <div class="write_btn_box">
          <button class="write_btn"><b>{{__('messages.write')}}</b></button>
      </div>
    </a> -->
    <div class="contents_2" id="paging">
        @include('components.main.pagination')
    </div>
    <a class="menuLink" id="li-1" href="{{route('board.create')}}">
      <div class="write_box">
        <center>
          <img src="{{asset('images/pic/write_icon_2.png')}}" class="write"/>
          <p id="write_text"><b>WRITE</b></p>
        </center>
      </div>
    </a>
</contents>
