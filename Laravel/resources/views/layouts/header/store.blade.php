<!--상단바-->
<center>
    <nav id="topMenu">
        <div id="logoBox">
            <a href="{{route('home')}}">
                <img src="{{asset('images/pic/rythmStore5.png')}}" id="logo"/>
            </a>
        </div>
        <ul>
            <li>
                <a class="menuLink" id="li-1" href="#">
                    <strong>
                        {{__('messages.order')}}
                        /
                        {{__('messages.delivery')}}</strong>
                </a>
            </li>
            <li>
                <a class="menuLink" href="{{route('cartPage')}}">
                    <strong>{{__('messages.cart')}}</strong>
                </a>
            </li>
            @if(\Auth::check())
            {{-- 로그아웃 --}}
                <a href="{{route('logout')}}" onclick="event.preventDefault();document.getElementById('logout-form').submit();">
                    <img src="{{asset('images/pic/user.png')}}" id="login-logo" />
                </a>
                <form id="logout-form" action="{{route('logout')}}" method="POST" style="display: none;">
                    @csrf
                </form>
            {{-- / 로그아웃 --}}
            @else
                <li>
                    <a class="menuLink" href="{{route('unity.loginPage')}}">
                        <strong>{{__('messages.login')}}
                        </strong>
                    </a>
                </li>
            @endif
        </ul>
    </nav>
</center>