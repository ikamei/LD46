
proc decr_suffix { str } {
    if { [regexp -nocase {_([0-9][0-9])} $str pattern num] } {
        scan $num %d myInteger
        incr myInteger -1
        set tmp [format "%02d" $myInteger]
        set str [regsub -all {_([0-9][0-9])} $str "_$tmp"]
        return $str
    }
    return ""
}

proc test { folder dest_folder} {
    set folders [glob -nocomplain -directory $folder -type d "*"]
#    aforeach sub_folder $folders {
#        test $sub_folder
#    }
#    puts "save_progress_db takes [expr [clock clicks -milliseconds] - $tick]ms"
    set filenames [glob -nocomplain -directory $folder -type f "*png"]
    foreach filename $filenames {
        if { [regexp -nocase {master_idol} $filename] } {
            set str [regsub -all {master_idol} $filename {master_pose1}]
            set str [decr_suffix $str]
            set str [file join $dest_folder [lindex [file split $str] end]]
            #puts "\t$str"
            #exec cp $filename $str
        } elseif { [regexp -nocase {master_speak} $filename] } {
            set str [regsub -all {master_speak} $filename {master_pose0}]
            set str [decr_suffix $str]
            set str [file join $dest_folder [lindex [file split $str] end]]
            #puts "\t$str"
            #exec cp $filename $str
        } elseif { [regexp -nocase {master_normal} $filename] } {
            set str [regsub -all {master_normal} $filename {master_pose2}]
            set str [decr_suffix $str]
            set str [file join $dest_folder [lindex [file split $str] end]]
            #puts "\t$str"
            #exec cp $filename $str
        } elseif { [regexp -nocase {master_agree} $filename] } {
            set str [decr_suffix $filename]
            set str [file join $dest_folder [lindex [file split $str] end]]
            #puts "\t$str"
            #exec cp $filename $str
        } elseif { [regexp -nocase {master_disagree} $filename] } {
            set str [decr_suffix $filename]
            set str [file join $dest_folder [lindex [file split $str] end]]
        } else {
            continue
        }
        puts "\t$str"
        exec cp $filename $str
    }
}

set folder "/Users/hh/Downloads/haha/4-19-1540/"
set dest_folder "/Volumes/untitled/git_vps/LD46/Assets/Resources/"
if true {
    test $folder $dest_folder
} else {
    set folder "/Volumes/untitled/sgt/"
    if { [regexp {[:| ]} $folder] } {
        puts "$folder"
    }
}
