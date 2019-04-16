<div class="select_box">
    <div class="select">
        <select name="job" id="sort" onchange="pagination({{$current_page}})">
            <option value="latest" id="latest" selected="selected">{{__('messages.latest')}}</option>
            <option value="hearts">{{__('messages.hearts')}}</option>
            <option value="hits">{{__('messages.hits')}}</option>
        </select>
    </div>
</div>