<template>
  <Card>
      <div class="padding-10">
      <Button class="search-btn" type="primary"><Icon type="search"/>&nbsp;&nbsp;搜索</Button>
    </div>
      <Table height="600" :columns="[
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
                ]" :data="articles"></Table>
                <Row class="padding-10 text-right"><Page :total="100" show-sizer @on-change="pageChange" @on-page-size-change="pageSizeChange" /></Row>
                
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>