<template>
  <Card>
      <Row :gutter="10" class="padding-10">
          <Col span="4"><Input v-model="serach.id" placeholder="序号" /></Col>
          <Col span="4"><Input v-model="serach.title" placeholder="标题" /></Col>
          <Col span="4"><Input v-model="serach.userName" placeholder="创建者" /></Col>
          <Col span="3"><DatePicker type="date" v-model="serach.createdDate" placeholder="创建时间" ></DatePicker></Col>
        <Button class="search-btn" type="primary" @click="getArticles" ><Icon type="search"/>&nbsp;&nbsp;搜索</Button>
    </Row>
      <Table :columns="[
                    {
                        title: '序号',
                        key: 'id'
                    },
                    {
                        title: '标题',
                        key: 'title'
                    },
                    {
                        title: '创建时间',
                        key: 'created',
                        render: (h,params)=>{
                            return h('div',
                                $moment(params.row.created).fromNow()
                            )
                        }
                    }
                ]" :data="articles.data"></Table>
                <Row class="padding-10 text-right"><Page :total="articles.total" show-sizer @on-change="pageChange" @on-page-size-change="pageSizeChange" /></Row>
                
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>