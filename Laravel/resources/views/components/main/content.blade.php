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
                        <a href="{{route('board', $ranking->id)}}">
                            @if($ranking->files[0])
                            <video width="100%">
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
                    <div class="view_like" id="view_like_{{$ranking->id}}">
                        @include('components.heart', ['board' => $ranking])
                    </div>
                    <a href="{{route('board', $ranking->id)}}">
                        <h4 id="title_text">
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

    <div class="select_box">
        <div class="select">
            <select name="job">
                <option value="인기순">인기순</option>
                <option value="최근순" selected="selected">최근순</option>
                <option value="조회순">조회순</option>
            </select>
        </div>
    </div>
    <div class="contents_2" id="paging">
        @include('components.main.pagination')
    </div>
</contents>