<contents>
    <div class="contents">
        <!--상단이미지-->
        <div class="img_box">
            <p id="title_text"><b><i>WRITE</i></b></p>
            <img src="{{asset('images/pic/bg_2.png')}}" id="bg_img" />
        </div>
        <!--글쓰기폼-->
        <center>
            <form action="{{route('board.store')}}" method="POST">
                @csrf
                <div class="form_group">
                    <div class="form_box">
                        <div class="name_box">
                            <p id="name"><b>{{__('messages.writer')}}</b></p><input type="text" class="name_input"
                                name="writer" value="{{\Auth::user()->name}}" readonly />
                        </div>
                        <div class="title_box">
                            <p id="title"><b>{{__('messages.writeTitle')}}</b></p><input type="text" class="title_input"
                                name="title" placeholder="{{__('messages.writeTitleHolder')}}" required />
                        </div>
                        <div class="comment_box">
                            <label for="comment" id="comment"><b>{{__('messages.writeContents')}}</b></label>
                            <textarea class="comment_input" name="content"
                                placeholder="{{__('messages.writeConHolder')}}" required></textarea>
                            <!--리스트-->
                            @include('components.writeForm.list')
                            <button type="submit" class="submit_btn"><b>{{__('messages.writeComplete')}}</b></button>
                        </div>
                    </div>
                </div>
            </form>
        </center>
    </div>
</contents>