   M S B A M L   `   `   `  ����Q  MStudyOfWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:StudyOfWpfApplication1StudyOfWpfApplication1  5   	   '    !StudyOfWpfApplication1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6local$clr-namespace:StudyOfWpfApplication1     5   	   -   5          � WindowStyle$
  None=�5   	    � AllowsTransparency .  � Title$ 灵悦音乐��5   	   $	��650��6   $
��1000��6#    � Icon$ /imgs/logo.ico��5	   	    � 	Resources 5
      (   "StudyOfWpfApplication1.BackgroundC   myConverter  myItemStyle �� 
TargetType �� Property �� Value �� 	Converter	 �� RelativeSource
 �� Triggers P� Property P� Value��%  &	        &	      7     5   
   62   �� 5   
   " ��6$   .�5      ��  � �� 5      �� 5      8   	 6   �� *Self+6F   5      
 5      P� 5       �@��
IsSelected . . �5      ��  � ��
 ����63   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      �� 5      $��75 �6   .+�5    
   � $��0��6   ���#a3d3fb.+�5!      �� -   5        $
��logo��5!   A   $��
39,5,904,5q�6   $��/imgs/head.jpeg��6(   6O   �� 5"      $��123,18,801,18q�6   
�������.��6>   音乐播放器6E   �� 5#      $��278,18,646,18q�6   
�������.��6>   查找歌曲6D   �� 5$      -   5        $��
search_key��5$   X   $��294,10,481,10q�6     CornerTextBox#��[ 6.   6X   �� 5%      -   5        $��search_music��5%   �   $��443,18,532,16q�6   $��25��6,   $��25��67   $��24(�6C   #��[" �6�   ��6Q   Z� ��6h   Z� .��5&      �� -   5        $��
Imqg_play1��5&      $��/imgs/search.ico��6+   $y�Visible=�6E   5*      � 5+      $��20��6   6!   �� 5,      -   5        $��Setlist��5,   z   $��30��6   $��30��6    $��868,15,102,15q�6,   $��24(�6Y   
����������#a3d3fb��6C   Z� .��5-      �� $��/imgs/moret.png��6   5.      �� 5/      -   5        $��30��5/      $��898,15,72,15q�6    $��30��66   $��24(�50   6   
����������#a3d3fb��6    Z� .��51      �� $��/imgs/smallt.png��6   52      �� 53      -   5        $��30��53      $��928,15,42,15q�6    $��30��66   $��24(�54   8   
����������	#FF31C373��6"   Z� ��55      �� 56      " ��6   .�57      ��  � �� 58      �� 59   "    �� 
TargetType" ��62   .R�5:   &   �� $��btnbg��6,   $��/imgs/closet.png��6:    �� Triggers 5;   &   P� 5<   *    �@J�IsMouseOver . . �5=   .   ��  �� 
TargetName$ btnbg��6c    � ��$ /imgs/redclose.png��6H   6c   5>   +   5@   #   5B      5C      5E      5F      � 5G   
   -	   5        $��g1��5G   -   $��2��6   $��1��6     5H      �� 5I      $	��86* �6    �� 5J      $	��17* �6     � ColumnDefinitions 5L      �� 5M      $	��130 �6#   �� 5N      $��70 �6#   �� 5O      $	��21* �6#   �� 5P      $	��19* �6#   .+�5R      �� $��1��6   $��7��6+   $��Right=�65   ���#BFBFBF$��2��6f   �� 5a      $��    我的音乐��6   $��Center=�6'   $	��Top=�6D   $	��188��6\   $��30��6h   $��16(�6t   ���	#00000000$��5,5,5,6q�6�   $��Light"�6�   $��5,25,7,0q�6�   $��2��6�   �� 5b      -
   5         ��Name$ 
btn_danru1��5b      $��	0,55,12,0q�6*   $	��Top=�6=   $��40��6U   $��0��6a   $��2��6�   ��5c      �� 5d      " ��6   .�5e      ��  � �� 5f      �� 5g   "   " ��62   .R�5h   &   �� $��btnbg��6,   $��/imgs/单曲1-1.png��69   $��0,0,1,0q�6R    5i   &   P� 5j   *    �@J�IsMouseOver . . �5k   .   �� $ btnbg��6`    � ��$ /imgs/单曲1-2.png��6H   6`   5l   +   P� 5m   *    � �� . . �5n   .   �� $ btnbg��6_    � ��$ /imgs/单曲1-3.png��6G   6_   5o   +   5q   #   5s      5t      5v      �� 5w      -   5        $ 
btn_danru2��5w      $��	0,95,12,0q�6*   $	��Top=�6=   $��40��6U   $��0��6a   $��2��6�   ��5x      �� 5y      " ��6   .�5z      ��  � �� 5{      �� 5|   "   " ��62   .R�5}   &   �� $��btnbg��6,   $��/imgs/歌手1-1.png��69    5~   &   P� 5   *    �@J�IsMouseOver . . �5�   .   �� $ btnbg��6`    � ��$ /imgs/歌手1-2.png��6H   6`   5�   +   P� 5�   *    � �� . . �5�   .   �� $ btnbg��6_    � ��$ /imgs/歌手1-3.png��6G   6_   5�   +   5�   #   5�      5�      5�      �� 5�      -   5        $ 
btn_danru3��5�      $��	0,135,0,0q�6*   $	��Top=�6=   $��40��6U   $��0��6a   $
��Left=�6�   $	��188��6�   $��2��6�   ��5�      �� 5�      " ��6   .�5�      ��  � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $��btnbg��6,   $��/imgs/专辑1.png��69    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   �� $ btnbg��6^    � ��$ /imgs/专辑2.png��6H   6^   5�   +   P� 5�   *    � �� . . �5�   .   �� $ btnbg��6]    � ��$ /imgs/专辑3.png��6G   6]   5�   +   5�   #   5�      5�      5�      �� 5�      $��    我的收藏��6   $��Center=�6'   $	��Top=�6D   $	��188��6\   $��30��6h   $��16(�6t   ���	#00000000$��5,5,5,6q�6�   $��Light"�6�   $��
10,190,2,0q�6�   $��2��6�   �� 5�      -   5        $ btn_collect_mus��5�      $��
0,225,12,0q�6/   $	��Top=�6C   $��40��6[   $��0��6g   $��2��6�   ��5�      �� 5�      " ��6   .�5�      ��  � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $��btnbg��6,   $��/imgs/单曲1-1.png��69    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   �� $ btnbg��6`    � ��$ /imgs/单曲1-2.png��6H   6`   5�   +   P� 5�   *    � �� . . �5�   .   �� $ btnbg��6_    � ��$ /imgs/单曲1-3.png��6G   6_   5�   +   5�   #   5�      5�      5�      �� 5�      -   5        $ btn_collect_alb��5�      $��
0,265,12,0q�6/   $	��Top=�6C   $��40��6[   $��0��6g   $��2��6�   ��5�      �� 5�      " ��6   .�5�      ��  � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $��btnbg��6,   $��/imgs/专辑1.png��69    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   �� $ btnbg��6^    � ��$ /imgs/专辑2.png��6H   6^   5�   +   P� 5�   *    � �� . . �5�   .   �� $ btnbg��6]    � ��$ /imgs/专辑3.png��6G   6]   5�   +   5�   #   5�      5�      5�      �� 5�      $��    我的歌单��6   $��Center=�6'   $	��Top=�6D   $	��106��6\   $��30��6h   $��16(�6t   ���	#00000000$��5,5,5,6q�6�   $��Light"�6�   $��
0,326,24,0q�6�   �� 5�      -   5        $ btn_tianjia��5�      $	��Top=�6,   $��35��6D   $��2��6P   $��73,326,57,0q�6d   ��5�      �� 5�      " ��6   .�5�      ��  � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $��btnbg��6,   $��/imgs/添加1.png��69    5�   &   P� 5�   *    �@J�IsMouseOver . . �5�   .   �� $ btnbg��6^    � ��$ /imgs/添加2.png��6H   6^   5�   +   P� 5�   *    � �� . . �5�   .   �� $ btnbg��6]    � ��$ /imgs/添加2.png��6G   6]   5�   +   5�   #   5�      5�      5�      R� 5�      -   5        $��tree_song_list��5�      $
��Left=�6-   $	��144��6H   $��
40,361,0,0q�6U   $	��Top=�6i   $	��148��5�      
�������$��2��6;   $��2��6O    � ContextMenu 5�      �� 5�      -   5        $��action_delete_list��5�   "   .@�5�      m� -   5        $��delete_list��5�   /   $��删除��6#   6B   5�      5�      5�      � 5�   
   -   5        $��Change2��5�      $��2��6   $y�Hidden=�6.   $	��800��6B   $	��515��6N   $��	200,0,0,0q�6[   $��1��6n   .+�5�      � $��2��6   $
��Left=�6#   $��	0,0,0,295q�6>   $	��800��6Q   .+�5�      �� $
��Left=�6   $	��170��63   $��	32,28,0,0q�6@   $	��Top=�6S   $	��170��6k   $��/imgs/beautiful.jpg ��6w   �� 5�      -   5        $��alb_name��5�   &   $��	专辑名��6   $
��Left=�66   $��
236,31,0,0q�6Q   $	��Top=�6e   $��48��6}   $	��564��6�   $��30(�6�   �� 5�      -   5        $��alb_mus_name��5�   &   $��歌手��6   $
��Left=�6:   $��
236,79,0,0q�6U   $	��Top=�6i   $��36��6�   $	��282��6�   $��22(�6�   �� 5�      -   5        $��btn_Collect_Alb��5�   �   $��236,166,0,22q�6   $
��Left=�6/   $��90��6J    J� RenderTransformOrigin$ 0.497,0.518&�6U   
�������.��5�      � .+�5�      �� $��/imgs/heart.ico��6    $��
-30,2,16,2q�69   $ 	0.5,0.567&�6M   $��15��6o   $��15��6z   �� 5�      $��收藏��6$   $��
-7,0,-31,0q�6.   $��16(�6B   $
��Left=�6P   $��39��6k   5�      5�      �� 5�      -   5        $��add_to_list_alb��5�   /   $��340,166,0,22q�6   $
��Left=�6F   $	��134��6a   $ 0.497,0.518&�6m   
�������.��5      � .+�5     �� $��/imgs/add.ico��6    $��
-32,2,60,2q�67   $ 0.5,0.5&�6K   $��15��6k   $��15��6v   �� 5     $��添加至歌单��6$   $��
-7,0,-31,0q�61   $��16(�6E   5     5     5     � 5     $��2��6   $
��Left=�6#   $��	0,248,0,6q�6>   $	��800��6Q   .+�5     �� $	��800��6   $��Right=�6+   ���	#FFF3F1F1$��6��6^   $��
0,-6,0,261q�6i   5	     �� 5
     -   5        $��alb_list��5
  \   $��0q�6H   ��6   Z� ��62   Z�  �� View 5     �� 5     .P�5     �� $��	歌曲名��6-    �� Width$	 300��6:    �� DisplayMemberBinding 6F   ��  �� Path$
 Name��� 5     $��歌手��6-   $	 150��69    6E   �� $	 Mus��� 5     $��时长��6-   $	 150��69    6E   �� $
 Time�5     5     5     5     � 5  
   -   5        $��Change1��5     $y�Visible=�6   $	��800��63   $	��515��6?   $��	200,0,0,0q�6L   $��1��6_   .+�5     � $��2��6   $
��Left=�6#   $��	0,0,0,416q�6>   $	��800��6Q   .+�5     �� $��52��6   $��30(�6$   $��67,11,603,36q�62   $ -0.196,0.663&�6H   $
��Bold"�6m   $��我的音乐��6   
����������	#FF723D3D5     �� 5     -   5        $��do_all_play_mus��5     $��0,63,573,10q�60   $��16(�6E   $��Right=�6S   $��89��6o   
�������$ 1.114,1.214&�6�   .��5     � .+�5     �� $��/imgs/play.ico��6    $��
-25,2,39,2q�68   $ 0.5,0.5&�6L   $��15��6l   $��15��6w   �� 5     $��播放全部��6$   $��
-9,0,-26,0q�60   5     5     �� 5      -   5        $��addMusic��5   �   $��
68,64,0,10q�6   $
��Left=�6-   $��60��6H   $ 0.497,0.518&�6S   
�������.��5!     � .+�5"     �� $��/imgs/add.ico��6    $��
-26,2,12,2q�67   $ 0.5,0.5&�6K   $��15��6k   $��15��6v   �� 5#     $��添加��6$   $��
-7,0,-31,0q�6.   $��16(�6B   5$     5%     �� 5&     -   5        $��add_to_list_all��5&  .   $��236,64,0,10q�6   $
��Left=�6E   $	��111��6`   $ 0.497,0.518&�6l   
�������.��5'     � .+�5(     �� $��/imgs/upload.ico��6    $��
-28,2,55,2q�6:   $ 0.5,0.5&�6N   $��15��6n   $��15��6y   �� 5)     $��添加到歌单��6$   $��
-9,0,-29,0q�61   $��16(�6E   5*     5+     5,     � 5-     $��2��6   $
��Left=�6#   $��	0,108,0,6q�6>   $	��800��6Q   .+�5.     �� $	��800��6   $��Right=�6+   ���	#FFF3F1F1$��6��6^   $��
0,-6,0,401q�6i   50     �� 51     -   5        $��all_list��51  \   $��0q�6H   #��[ 6�   ��6   Z� ��62   Z�  52     �� 53     .P�54     �� $��	歌曲名��6-   $	 300��6:    6F   �� $
 Name��� 55     $��歌手��6-   $	 150��69    6E   �� $	 Mus��� 56     $��专辑信息��6-   $	 200��6;    6G   �� $	 Alb��� 57     $��时长��6-   $	 150��69    6E   �� $
 Time�58      5:     �� 5;     -   5        $��action_delete��5;  &   .@�5<     m� -   5        $��delete��5<  3   $��删除��6'   6A   5=     5?     5@     5A     � 5E  
   -    5        $��Change4��5E     $y�Hidden=�6   $	��800��62   $	��515��6>   $��	200,0,0,0q�6K   $��1��6^   .+�5F     � $��2��6   $
��Left=�6#   $��	0,0,0,416q�6>   $	��800��6Q   .+�5G     �� -!   5        $��change_mylist��5G  $   $��52��6   $��30(�69   $��67,11,58,36q�6G   $ -0.196,0.663&�6\   $
��Bold"�6�   $��我的歌单��6�   
����������	#FF723D3D5H     �� 5I     -"   5        $��
0,68,630,5q�5I     $��16(�6-   $��Right=�6;   $��98��6W   
�������$ 1.114,1.214&�6u   .��5J     � .+�5K     �� $��/imgs/play.ico��6    $��
-27,2,30,2q�68   $ 0.5,0.5&�6L   $��15��6l   $��15��6w   �� 5L     $��播放全部��6$   $��
-5,0,-30,0q�60   5M     5N     5O     � 5P     $��2��6   $
��Left=�6#   $��	0,108,0,6q�6>   $	��800��6Q   .+�5Q     �� $	��800��6   $��Right=�6+   ���	#FFF3F1F1$��6��6^   $��
0,-6,0,401q�6i   5S     �� 5T     -#   5        $��my_song_list��5T  \   $��0q�6H   ��6   Z� ��62   Z�  5U     �� 5V     .P�5W     �� $��	歌曲名��6-   $	 300��6:    6F   �� $
 Name��� 5X     $��歌手��6-   $	 150��69    6E   �� $	 Mus��� 5Y     $��专辑信息��6-   $	 200��6;    6G   �� $	 Alb��� 5Z     $��时长��6-   $	 150��69    6E   �� $
 Time�5[     5]     5^     5_     � 5b  
   -$   5        $��Change3��5b     $y�Hidden=�6   $	��800��62   $	��515��6>   $��	200,0,0,0q�6K   $��1��6^   .+�5c     � $��2��6   $
��Left=�6#   $��	0,0,0,416q�6>   $	��800��6Q   .+�5d     �� -%   5        $��
change_mus��5d  �   $��52��6   $��30(�6$   $��67,11,603,36q�62   $ -0.196,0.663&�6H   $
��Bold"�6m   $��我的歌手��6   
����������	#FF723D3D5e     �� 5f     -&   5        $��
0,64,655,9q�5f     $��16(�6-   $��Right=�6;   $��92��6W   
�������$ 1.114,1.214&�6u   .��5g     � .+�5h     �� $��/imgs/play.ico��6    $��
-20,5,34,0q�68   $ 0.5,0.5&�6L   $��15��6l   $��15��6w   �� 5i     $��播放全部��6$   $��
-5,0,-30,0q�60   5j     5k     �� 5l     -'   5        $��add_to_list_mus��5l  .   $��176,63,0,11q�6   $
��Left=�6E   $	��117��6`   $ 0.497,0.518&�6l   
�������.��5m     � .+�5n     �� $��/imgs/upload.ico��6    $��
-60,2,15,2q�6:   $ 0.5,0.5&�6N   $��15��6n   $��15��6y   �� 5o     $��添加到歌单��6$   $��
-7,0,-31,0q�61   $��16(�6E   5p     5q     5r     � 5s     $��2��6   $
��Left=�6#   $��	0,108,0,6q�6>   $	��800��6Q   .+�5t     �� $	��800��6   $��Right=�6+   ���	#FFF3F1F1$��6��6^   $��
0,-6,0,401q�6i   5v     �� 5w     -(   5        $��mus_list��5w  \   $��0q�6H   ��6   Z� ��62   Z�  5x     �� 5y     .P�5z     �� $��	歌曲名��6-   $	 300��6:    6F   �� $
 Name��� 5{     $��专辑信息��6-   $	 200��6;    6G   �� $	 Alb��� 5|     $��时长��6-   $	 150��69    6E   �� $
 Time�5}     5     5�     5�     � 5�  
   -)   5        $��Change5��5�     $y�Hidden=�6   $	��800��62   $	��515��6>   $��1��6L    J� Opacity$	 0.8Q�6Z   .+�5�     �� -*   5        $��
BaseLineSP��5�  ;   $��28,0,20,208q�6   $��1��6.   .+�5�     �� -+   5        $��BaseLine��5�  �   
��� ���$��2��6'    �� StrokeDashArray$	 4,2R�6;   	 �� X1$ 0��6Q   	 �� Y2$ 0��6�   $y�	Collapsed=�6�   	 �� Y1$ 0��6�   $��2��6�   $��4��6  	 �� X2 6X   ��  �� ElementName$ CanvasLyric��$ ActualWidth���6�   Z�   J� OpacityMask  6�   Z� 6  �� 5�     -,   5        $��
tbDragTime��5�     $��0.00.00��6.   ! �� TextAlignment$
! Left=�6=   
��� ���$��14(�6f   $y�	Collapsed=�6t   ��6�   �� $ CanvasLyric��$ ActualWidth�5�     �� 5�     --   5        " ��Name$" CanvasLyric��5�     }�. ���#5A5758.+�5�     �� -.   5        # ��Name$# StackPanelCommonLyric��5�     $ �� Left$$ 0��6=   
% �� Top$	% 300��6M   
  �   ���6^   �� *ActualWidth+$ CanvasLyric��& �� Mode$& OneWay=�6�   �� 5�     -/   5        $" CanvasFocusLyric��5�     $��36��64   $$ 0��6A   $	% 253��6Q   
  �   �}�. ��6b   �� *ActualWidth+$ CanvasLyric��$& OneWay=���6�   Z� .+�5�     �� -0   5        ' ��Name$' TBFocusLyricBack��5�      $"��听音乐,就用QQ播放器!��6:   $��22(�6�   $! Center=�6�   
�������
  �   ���6N   �� *Height+$ CanvasFocusLyric����6�   �� *ActualWidth+$ CanvasLyric��$& OneWay=�6  �� 5�     -1   5        $" CanvasFocusLyricFore��5�     $��30��6s   }�. 
  �   ���6;   �� *Height+$ CanvasFocusLyric��.+�5�     �� -2   5        $' TBFocusLyricFore��5�  $   
��� ���
  �   ���6>   �� *Text+$ TBFocusLyricBack����6r   �� *Height+$ CanvasFocusLyric����6�   �� *
FontSize+$ TBFocusLyricBack��! 6�   �� *TextAlignment+$ TBFocusLyricBack����6@  �� *
FontFamily+$ TBFocusLyricBack����6�  �� *ActualWidth+$ CanvasLyric��$& OneWay=�y�6�  �� *
Visibility+$ TBFocusLyricBack����6  �� *TextWrapping+$ TBFocusLyricBack����6H  �� *TextTrimming+$ TBFocusLyricBack��6�  5�     5�     5�     5�     � 5�  
   $��2��6   ���#059cd4.+�5�     �� -3   5        $ 
btn_former��5�     $��30,25,930,0q�6*   $	��Top=�6?   $��40��6W   $��0��6c   
���������5�     �� 5�     " ��6   .�5�     ��  � �� 5�     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg��6,   $��/imgs/上一首1-1 WC.png��69    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg��6a    � ��$ /imgs/上一首1-2.png��6H   6a   5�  +   P� 5�  *    � �� . . �5�  .   �� $ btnbg��6c    � ��$ /imgs/上一首1-1 WC.png��6G   6c   5�  +   5�  #   5�     5�     5�     �� 5�     -4   5        $ btn_play��5�     $��75,16,870,0q�6)   $	��Top=�6>   $��55��6V   $��0��6b   ��6�   Z� ��6�   Z� ��6�   Z� .��5�     �� -5   5        $��	Img_play1��5�     $��imgs/播放2.png��6*   $y�Visible=�6@   ��5�     �� 5�     " ��6   .�5�     ��  � �� 5�     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg��6,   $��/imgs/播放1-2.png��69    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg��6`    � ��$ /imgs/播放1-1.png��6H   6`   5�  +   5�  #   5�     5�     5�     �� 5�     -6   5        $ btn_next��5�     $��135,25,825,0q�6(   $	��Top=�6>   $��40��6V   ��5�     �� 5�     " ��6   .�5�     ��  � �� 5�     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg��6,   $��/imgs/下一首1-1.png��69    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg��6a    � ��$ /imgs/下一首1-2.png��6H   6a   5�  +   P� 5�  *    � �� . . �5�  .   �� $ btnbg��6`    � ��$ /imgs/下一首1-1.png��6G   6`   5�  +   5�  #   5�     5�     5�     �� 5�     -7   5        $ btn_music_lyric��5�     $��258,15,687,0q�6/   $	��Top=�6E   $��55��6]   $��0��6i   ��5�     �� 5�     " ��6   .�5�     ��  � �� 5�     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg��6,   $��/imgs/播放歌曲.png��69    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg��6_    � ��$ /imgs/播放歌曲.png��6H   6_   5�  +   P� 5�  *    � �� . . �5�  .   �� $ btnbg��6^    � ��$ /imgs/播放歌曲.png��6G   6^   5�  +   5�  #   5�     5�     5�     �� 5�     -8   5        $ btn_collect��5�     $��890,36,89,0q�6+   $	��Top=�6@   $��20��6X   ��5�     �� 5�     " ��6   .�5�     ��  � �� 5�     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg��6,   $��/imgs/收藏2-4.png��69    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg��6`    � ��$ /imgs/收藏2-2.png��6H   6`   5�  +   P� 5�  *    � �� . . �5   .   �� $ btnbg��6_    � ��$ /imgs/收藏2-3.png��6G   6_   5  +   5  #   5     5     5     �� 5	     -9   5        $ 	btn_voice��5	     $��800,35,175,0q�6)   $	��Top=�6?   $��21��6W   ��5
     �� 5     " ��6   .�5     ��  � �� 5     �� 5  "   " ��62   .R�5  &   �� $��btnbg��6,   $��/imgs/声音1-1.png��69    5  &   P� 5  *    �@J�IsMouseOver . . �5  .   �� $ btnbg��6`    � ��$ /imgs/声音1-2.png��6H   6`   5  +   P� 5  *    � �� . . �5  .   �� $ btnbg��6_    � ��$ /imgs/声音1-1.png��6G   6_   5  +   5  #   5     5     5     �� 5     -:   5        $ btn_playmode��5     $��847,35,128,0q�6,   $	��Top=�6B   $��21��6Z   ��5     �� 5      " ��6   .�5!     ��  � �� 5"     �� 5#  "   " ��62   .R�5$  &   �� $��btnbg��6,   $��/imgs/顺序播放1-1.png��69    5%  &   P� 5&  *    �@J�IsMouseOver . . �5'  .   �� $ btnbg��6b    � ��$ /imgs/顺序播放1-2.png��6H   6b   5(  +   P� 5)  *    � �� . . �5*  .   �� $ btnbg��6a    � ��$ /imgs/顺序播放1-1.png��6G   6a   5+  +   5-  #   5/     50     52     � 53     -;   5        $
��m_pg��53  T   $
��Left=�6   $��8��65   $��
329,46,0,0q�6@   $	��Top=�55     $	��370��62   
���������#059cd4( � Value( 54     �� $ jindu��$ Content�) �� UpdateSourceTrigger$) PropertyChanged=�5=     �� 5>     -<   5        $ btn_playingList��5>     $��757,35,220,0q�6/   $	��Top=�6E   $��22��6]   $��0��6i   ��5?     �� 5@     " ��6   .�5A     ��  � �� 5B     �� 5C  "   " ��62   .R�5D  &   �� $��btnbg��6,   $��/imgs/列表2-1.png��69    5E  &   P� 5F  *    �@J�IsMouseOver . . �5G  .   �� $ btnbg��6`    � ��$ /imgs/列表2-2.png��6H   6`   5H  +   P� 5I  *    � �� . . �5J  .   �� $ btnbg��6_    � ��$ /imgs/列表2-1.png��6G   6_   5K  +   5M  #   5O     5P     5R     � 5T     -=   5        * �Name$* Pop_playlist��5T     $
��Fade=�6*   $	��278��6@   $	��155��6L   $��10,100,0,0 q�5U     + � AllowsTransparency+ . , � 	StaysOpen, .  ��.  $	��Top=�6^   - � PlacementTarget- 5T  Z   �� $ btn_playingList��.��5V     �� ->   5        $��box_playlist��5V     ���#059cd4
�������$	��250��6V   5Y     5Z     � 5[     -?   5        $* Pop_Play_State��5[     $
��Fade=�6,   $��80��6B   $��80��6M   + . , .  ��.  $	��Top=�6�   - 6Y   �� $ btn_playmode��.��5\     �� -@   5        $��my_play_state��5\     ���#059cd4
�������.@�5]     �� -A   5        $��random��5]  "   .��6[   随机播放6a   �� 5^     -B   5        $��order��5^  "   .��6Y   顺序播放6_   �� 5_     -C   5        $��single��5_  "   .��6[   单曲循环6a   5`     5a     � 5b     -D   5        $* 
Pop_danru2��5b     $
��Fade=�6(   $��88��6>   $	��125��6I   $��Mouse=�5c     + . , .  ��.  - 5b  V   �� $ 
btn_danru2��.��5d     �� -E   5        $��my_mus��5d     $	 0.8Q�6S   5e     � 5f     -F   5        $* 
Pop_danru3��5f     $
��Fade=�6(   $��88��6>   $	��125��6I   $��Mouse=�5g     + . , .  ��.  - 5f  V   �� $ 
btn_danru3��.��5h     �� -G   5        $��my_album��5h     $	 0.8Q�6W   5i     � 5j     -H   5        $* Pop_alb_collect��5j     $
��Fade=�6-   $��88��6C   $	��125��6N   $��Mouse=�5k     + . , .  ��.  - 5j  [   �� $ btn_collect_alb��.��5l     �� -I   5        $��my_album_collect��5l     62   5m     � 5n     -J   5        $* Pop_show_list��5n     $
��Fade=�6+   $��88��6A   $	��125��6L   $��Mouse=�5o     + . , .  ��.  - 5n  Y   �� $ add_to_list_all��.��5p     �� -K   5        $��my_list_show��5p     6.   5q     � 5r     -L   5        $* 
Pop_volume��5r     $
��Fade=�6(   $��50��6>   $	��125��6I   $	��Top=�5s     + . , .  ��.  - 5r  V   �� $ 	btn_voice��.��5t     �� -M   5        $��slider_volume��5u     . �� Orientation$. Vertical=�5t     $	��125��60   $��	15,0,10,0q�6=   5u  .   5v     �� 5w     -N   5        $��	show_time��5w  =   $��00:00/00:00��6   
�������$
��Left=�6N   $��
622,15,0,0q�6i   $	��Top=�6}   �� 5x     -O   5        $��jindu��5x      $��0��6   $
��Left=�6-   $��
830,19,0,0q�6H   $	��Top=�6\   $y�Hidden=�6t   �� 5y     -P   5        $��info_mus��5y  $   $��歌曲-歌手��6   $
��Left=�64   $��
329,15,0,0q�6O   $	��Top=�6c   $	��288��6{   
�������6�   �� 5{     -Q   5        $��desLyric��5{     $��930,35,46,0q�6=   $	��Top=�6R   $��22��6j   $��0��6v   ��5|     �� 5}     " ��6   .�5~     ��  � �� 5     �� 5�  "   " ��62   .R�5�  &   �� $��btnbg1��6,   $��/imgs/灰词.png��6:    5�  &   P� 5�  *    �@J�IsMouseOver . . �5�  .   �� $ btnbg1��6]    � ��$ /imgs/白词.png��6H   6]   5�  +   5�  #   5�     5�     5�     5�     5�     5�     