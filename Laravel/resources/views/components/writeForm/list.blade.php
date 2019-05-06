<div class="container">
    <br />
        <div class="row">
                <p id="list"><b>{{__('messages.list')}}</b></p>
            <div class="list_box">
            <div class="well text-right">
                <div class="row">
                    <div class="col-md-10">
                        <div class="input-group">
                            <span class="input-group-addon glyphicon glyphicon-search"></span>
                            <input type="text" name="SearchDualList" class="form-control" placeholder="search" />
                        </div>
                    </div>
                    <div class="col-md-2">
                    <div class="btn-group">
                        <a class="btn btn-default selector" title="select all"><i class="glyphicon glyphicon-unchecked"></i></a>
                    </div>
                    </div>
                </div>
                <ul class="list-group" id="list-group">
                    @include('components.writeForm.item')
                </ul>
            </div>
        </div>
    </div>
</div>