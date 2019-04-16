<?php

use Illuminate\Database\Seeder;

class FilesTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=10; $i++){
            $user_id = random_int(1, DB::table('users')->count());
            DB::table('files')->insert([
                'user_id' => $user_id,
                'path' => "https://s3.ap-northeast-2.amazonaws.com/capstone.rhythmtataki.bucket/workshop/drumSoundClip/".$user_id."/",
                'name' => "test".$i.".ogg",
                'type' => "ogg",
                'size' => 10*$i,
                'created_at' => now()
            ]);
        }
    }
}
