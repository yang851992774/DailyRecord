# DailyRecord
故不积跬步，无以至千里；不积小流，无以成江海。骐骥一跃，不能十步；驽马十驾，功在不舍。锲而舍之，朽木不折；锲而不舍，金石可镂

## 目标

- 每周最少2个学习计划，坚持就是胜利。
- 每次完成一个任务需要进行总结。
- 定期复盘。



## 计划

| 序号 | 学习内容                                                     | 截止日期  | 目录          |
| ---- | :----------------------------------------------------------- | --------- | ------------- |
| 1    | [2D物理画线](https://linxinfa.blog.csdn.net/article/details/114700727) | 2021-12-5 | 2D/LineDrawer |
| 2    | [Unity制作方阵编队](https://blog.csdn.net/linxinfa/article/details/119928677?spm=1001.2014.3001.5501) | 2021-12-8 | 3D/Formation  |
|      |                                                              |           |               |
|      |                                                              |           |               |



## 总结归纳

### 1、2D物理画线

###### LineRender组件

Position：坐标点；
Width：线宽度；
Color：线颜色（支持渐变）；
Corner Vertices：拐弯处的顶点数量（让拐弯圆滑一点）；
End Cap Vertices：线段头尾的顶点数量（让线段头尾圆滑一点）；
Use World Space：是否使用世界坐标（不要勾选）；
Materias：材质球。

###### EdgeCollider2D组件

Edge Radius：边界碰撞体的半径。
Points：边界碰撞体的坐标点（要与LineRenderer的点一致）。
每新增一个点需要加CircleCollider2D组建，为了让画的线碰撞比较平滑。
