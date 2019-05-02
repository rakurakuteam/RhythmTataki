@extends('master')

@section('title')
    소리공방
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/workshop/workshop.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
@endsection

@section('header')
    {{header("Access-Control-Allow-Origin: *")}}
    @include('layouts.header.main')
<div id="dropZone">
@endsection

@section('body')
    <div class="container" id="workshop">
        @include('layouts.body.workshop')
    </div>
@endsection

@section('footer')
</div>
@include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="https://unpkg.com/wavesurfer.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.regions.min.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.cursor.min.js"></script>
    <script src="{{asset('js/workshop/player.js')}}"></script>
    <script src="{{asset('js/workshop/upload.js')}}"></script>
@endpush
