   M S B A M L   `   `   `  ����  StudyOfWpfApplication1)    #StudyOfWpfApplication1.DeskLyricWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � AllowsTransparency  .  � Title$ DeskLyricWin��5   	   $	��120��6   $
��1200��6+    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�6   ���	#00000000 � ShowInTaskbar .   � Topmost . .��5      � ���	#00000000$��93��6"   $
��1182��6.   .+�5   
   �� -   5         ��Name$ rectangleDeskLyricBack��5   q   $
��Left=�6V   
���   �$	��Top=�6�   ���#5A5758 J� Opacity$	 0.8Q�6  $y�Hidden=�6  $��1��6)  ��6   ��  �� ElementName$ canvasDeskLyric��	 �� Path$	 ActualHeight���6�   �� $ canvasDeskLyric��$	 ActualWidth�6)  �� 5	   
   -   5        $��canvasDeskLyric��5	   )   $��81��6   $��0q�6   
 J� OpacityMask

 �   ����	#00000000.+�5
      �� -   5         ��Name$ textBlockDeskLyricBack��5
      $$��听音乐，用灵悦音乐！��68   $��40(�6J    �� TextAlignment$ Center=�6X   ���#FFD4BB���	#00000000��6�   �� $ canvasDeskLyric��$	 ActualWidth���6�   �� $ canvasDeskLyric��$	 ActualHeight��� 5      -   5         ��Name$ canvasDeskLyricFore��5   r   $
��Left=�6W   $	��Top=�6�   $
��1177��6�   }�. ���	#00000000$y�Visible=�6�   ��6   �� $ canvasDeskLyric��$	 ActualHeight�.+�5      �� -   5        $ textBlockDeskLyricFore��5   ^   ���#8CCEF1$ Center=�6f  ���	#00000000

 �   ���6   �� $ canvasDeskLyric��$	 ActualHeight���6~   �� $ textBlockDeskLyricBack��$
	 Text���6�   �� $ canvasDeskLyric�� �� Mode$ OneWay=�$	 ActualWidth���6  �� $ textBlockDeskLyricBack��$	 FontSize���6}  �� $ textBlockDeskLyricBack��$	 
FontFamily� 6�  �� $ textBlockDeskLyricBack��$	 Opacity�5      5      �� 5   
   -   5        $��	btn_close��5   3   $��×��6   $y�Hidden=�6D   $
��Left=�6X   $��1158,0,0,71q�6s   $��Bottom=�6�   $��24��6�   5      5      