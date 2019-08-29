{{--  Modal --}}
{{-- loginModal --}}
<div id="loginModal" class="modal fade">
    <div class="modal-dialog modal-login">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{__('messages.login')}}</h4>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            </div>
            <div class="modal-body">
                <form action="{{route('login')}}" method="post">
                    @csrf
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="E-mail" name="email" required="required">
                    </div>
                    <div class="form-group">
                        <input type="password" class="form-control" placeholder="Password" name="password"
                            required="required">
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btn btn-primary btn-block btn-lg" value="{{__('messages.login')}}">
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
{{-- joinModal --}}
<div id="joinModal" class="modal fade">
    <div class="modal-dialog modal-login">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{__('messages.join')}}</h4>
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            </div>
            <div class="modal-body">
                <form action="{{route('register')}}" method="post">
                    @csrf
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Name" name="name" value="{{old('name')}}"
                            required="required">
                    </div>
                    @if ($errors->has('name'))
                    <span class="invalid-feedback" role="alert" style="margin-bottom: 20px">
                        <strong style="color: red;">{{ $errors->first('name') }}</strong>
                    </span>
                    @endif
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="E-mail" name="email"
                            value="{{old('email')}}" required="required">
                    </div>
                    @if ($errors->has('email'))
                    <span class="invalid-feedback" role="alert" style="margin-bottom: 20px">
                        <strong style="color: red;">{{ $errors->first('email') }}</strong>
                    </span>
                    @endif
                    <div class="form-group">
                        <input type="password" class="form-control" placeholder="Password" name="password"
                            required="required">
                    </div>
                    @if ($errors->has('password'))
                    <span class="invalid-feedback" role="alert" style="margin-bottom: 20px">
                        <strong style="color: red;">{{ $errors->first('password') }}</strong>
                    </span>
                    @endif
                    <div class="form-group">
                        <input type="password" class="form-control" placeholder="Password_confirmation"
                            name="password_confirmation" required="required">
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btn btn-primary btn-block btn-lg" value="{{__('messages.join')}}">
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
{{-- /joinModal --}}
{{-- /Modal --}}
<style type="text/css">
    body {
        font-family: 'Varela Round', sans-serif;
    }

    .modal-login {
        width: 350px;
    }

    .modal-login .modal-content {
        padding: 20px;
        border-radius: 1px;
        border: none;
    }

    .modal-login .modal-header {
        border-bottom: none;
        position: relative;
        justify-content: center;
    }

    .modal-login h4 {
        text-align: center;
        font-size: 26px;
    }

    .modal-login .form-group {
        margin-bottom: 20px;
    }

    .modal-login .form-control,
    .modal-login .btn {
        min-height: 40px;
        border-radius: 30px;
        font-size: 15px;
        transition: all 0.5s;
    }

    .modal-login .form-control {
        font-size: 13px;
    }

    .modal-login .form-control:focus {
        border-color: #a177ff;
    }

    .modal-login .hint-text {
        text-align: center;
        padding-top: 10px;
    }

    .modal-login .close {
        position: absolute;
        top: -5px;
        right: -5px;
    }

    .modal-login .btn {
        background: #a177ff;
        border: none;
        line-height: normal;
    }

    .modal-login .btn:hover,
    .modal-login .btn:focus {
        background: #8753ff;
    }

    .modal-login .hint-text a {
        color: #999;
    }

    .trigger-btn {
        display: inline-block;
        margin: 100px auto;
    }
</style>