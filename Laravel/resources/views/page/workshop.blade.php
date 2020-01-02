@extends('master')

@section('title')
    소리공방
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/workshop/workshop.css')}}" />
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" /> --}}
    <link rel="stylesheet" type="text/css" href="{{asset('css/main/main.css')}}" />
@endsection

@section('head')
    {{header("Access-Control-Allow-Origin: *")}}
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('body')
    <div class="container" id="workshop">
        <div class="workshop_jumbo" id="workshop_jumbo">
            <img src="{{asset('images/pic/workshop_jumbo.png')}}" alt="jumbtron" id="workshop_jumbo_img">
        </div>
        @include('layouts.body.workshop')
    </div>
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="https://unpkg.com/wavesurfer.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.regions.min.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.cursor.min.js"></script>
    <script src="{{asset('js/workshop/player.js')}}"></script>
    <script src="{{asset('js/workshop/upload.js')}}"></script>
@endpush
