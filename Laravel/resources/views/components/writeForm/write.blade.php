<div class="comment_box">
<div class="contents">
            <!--상단이미지-->
            <div class="img_box">
                <p id="title_text"><b><i>WRITE</i></b></p>
                <img src="pic/drum_img.png" id="drum_img" />
            </div>
            <!--글쓰기폼-->
            <center>
                <div class="form_group">
                    <div class="form_box">
                        <div class="name_box">
                            <p id="name"><b>{{__('messages.writer')}}</b></p><input type="text" class="name_input" placeholder="{{__('messages.writerHolder')}}" />
                        </div>
                        <div class="title_box">
                            <p id="title"><b>{{__('messages.writeTitle')}}</b></p><input type="text" class="title_input" placeholder="{{__('messages.writeTitleHolder')}}" />
                        </div>
                            <label for="comment" id="comment"><b>{{__('messages.writeContents')}}</b></label>
                            <textarea class="comment_input" placeholder="{{__('messages.writeConHolder')}}"></textarea>
                              @include('components.writeForm.list')
                            <button type="submit" class="submit_btn"><b>{{__('messages.writeComplete')}}</b></button>
                    </div>
                </div>
            </center>
        </div>
</div>
