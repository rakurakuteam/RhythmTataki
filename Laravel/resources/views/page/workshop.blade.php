@extends('master')

@section('title')
    소리공방
@endsection

@section('nav')
<div id="dropZone">
    <div>{{__('messages.navbar')}}</div>
@endsection

@section('body')
    <div class="container" id="workshop">
        @include('layouts.workshop.body')
    </div>
@endsection

@section('footer')
    <div>{{__('messages.footer')}}</div>
</div>
@endsection

@push('scripts')
    <script src="https://unpkg.com/wavesurfer.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.regions.min.js"></script>
    <script src="https://unpkg.com/wavesurfer.js/dist/plugin/wavesurfer.cursor.min.js"></script>
@endpush