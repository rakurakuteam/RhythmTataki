@extends('master')

@section('title')
    소리공방
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/board/board.css')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
<div id="dropZone">
@endsection

@section('nav')
@endsection

@section('body')
    <div class="container" id="workshop">
        @include('layouts.body.workshop')
    </div>
@endsection

@section('footer')
</div>
@endsection

@push('scripts')
    <script src="https://unpkg.com/wavesurfer.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.regions.min.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.cursor.min.js"></script>
@endpush