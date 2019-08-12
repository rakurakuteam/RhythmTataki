<form name="uploadForm" id="uploadForm" enctype="multipart/form-data" method="post">
  <center>
    <div class="contents_box">
      <div class="middle_box">
        <img src="{{asset('images/pic/album.png')}}" class="album_img">
        <h1 class="title_text">{{__('messages.file_select_message')}}</h1>
      </div>
      <div class="filebox">
        <label for="ex_file"><div id="file_select_text"><b>{{__('messages.file_select')}}</b></div></label>
        <input type="file" name="file" id="ex_file" onchange="selectFile()">
      </div>
    </div>
  </center>
</form>
