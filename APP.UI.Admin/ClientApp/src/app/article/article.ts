import { Component, Vue, Prop } from 'vue-property-decorator';
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css'
import { quillEditor } from 'vue-quill-editor';
import 'quill/dist/quill.core.css';
import 'quill/dist/quill.snow.css';

import './article.less';
import {
  ArticleModel, ArticleListModel, SearchArticleModel,
  Cascader, Result, ChannelModel
} from '@/api-client/client';
import { _apiUrl, _Article, _File } from '@/api-client';
import { StateTransformer } from 'vuex-class/lib/bindings';

@Component({
  components: {
    mavonEditor,
    quillEditor
  },
})

export default class ArticleComponent extends Vue {
  private $moment: any;
  private articleForm: any = { showDrawer: false, title: '', disabledEditorSwitch: false, disabledEditorTooltip: true };
  private articles: Result<ArticleListModel[]> = new Result<ArticleListModel[]>();
  private serach: SearchArticleModel = new SearchArticleModel();
  private article: ArticleModel = new ArticleModel();
  private channel: ChannelModel = new ChannelModel();
  private channels: Cascader[] = new Array<Cascader>();
  private quillOptions = {
    placeholder: '开始编辑...',
  };
  private articleFormRules: any = {
    title: [{ required: true, trigger: 'blur', message: '请输入文章标题' }],
    channelId: [{ required: true, trigger: 'on-visible-change', type: 'array', message: '请选择文章栏目' }]
  };

  private mounted() {
    this.getArticles();
  }
  // 交互逻辑
  private getArticles() {
    this.serach.take = this.serach.take || 10;
    this.serach.createdDate = this.serach.createdDate
      && new Date(this.$moment(this.serach.createdDate).format('YYYY-MM-DD'));
    _Article.getArticles(this.serach).then(r => this.articles = r);
  }
  private editArticle(id: string) {
    this.articleForm.showDrawer = true;
    this.getChannels();
    if (!id) {
      if (this.article.id) { this.article = new ArticleModel(); }
      this.articleForm.title = '创建文章';
      return;
    }
    this.articleForm.title = '修改文章';
    _Article.getArticle(id).then(r => this.article = r.data as ArticleModel);
  }
  private delArticle(id: string) {
    _Article.delArticle(id).then(r => {
      if (r.status) {
        this.getArticles();
        this.$Notice.success({
          title: '删除成功。'
        });
      }
    });
  }

  private getChannels() {
    _Article.getChannelsToCascader('').then(r => this.channels = r);
  }
  private getChildrenChannels(item: any, callback: any) {
    item.loading = true;
    _Article.getChannelsToCascader(item.value)
      .then(r => { item.children = r; item.loading = false; callback(); });
  }
  private addChannel() {
    if (this.channel && this.article.channelId) {
      this.channel.parentId = this.article.channelId.slice(-1)[0];
      _Article.addChannel(this.channel).then(r => {
        let channels = this.article.channelId || new Array<string>();
        let currItem = this.findCascaderItemByValue(channels);
        this.getChildrenChannels(currItem, () => {
          if (r.status && r.data) {
            channels.push(r.data);
          }
        });
      });
    }
  }

  private submitArticle(): void {
    const form: any = this.$refs.articleForm;
    form.validate((valid: any) => {
      if (valid) {
        if (this.article.id) {
          _Article.updateArticle(this.article);
        } else {
          _Article.addArticle(this.article).then(r => {
            if (r.status) {
              this.articleForm.showDrawer = false;
              this.article = new ArticleModel();
              this.getArticles();
              this.$Notice.success({
                title: '保存成功。'
              });
            }
          });
        }
      }
    });
  }
  private findCascaderItemByValue(values: string[]) {
    let channels = this.channels;
    let item: Cascader = new Cascader();
    values.forEach(f => {
      if (values.indexOf(f) === 0) {
        item = channels.filter(m => m.value === f)[0]
      } else {
        if (values.indexOf(f) === values.length - 1) {
          item = item.children && item.children.filter(m => m.value === f)[0] || new Cascader();
        } else {
          item = item.children && item.children.filter(m => m.value === f)[0] || new Cascader();
        }
      }
    });
    return item;
  }
  // UI变化控制
  // todo

  // 编辑器逻辑
  private editorSwitch(isDisabled: boolean) {
    this.articleForm.disabledEditorSwitch = isDisabled;
    this.articleForm.disabledEditorTooltip = !isDisabled;
  }
  private mavonEditorChange(value: string) {
    this.editorSwitch(!!value);
  }
  private mavonImgAdd(pos: any, $file: any) {
    _File.uploadImg({ fileName: $file.name, data: $file }).then((r) => {
      if (r.status && r.data) {
        let img = _apiUrl + r.data.path + r.data.name;
        (this.$refs.mavon as any).$imgUpdateByUrl(pos, img);
      }
    });
  }
  private mavonImgDel(pos: any) {
    // 远程图片删除逻辑
  }

  private quillEditorChange(value: string) {
    this.editorSwitch(!!value);
  }
  // Table分页
  private pageChange(page: number) {
    this.serach.skip = --page * this.serach.take;
    this.getArticles();
  }
  private pageSizeChange(size: number) {
    this.serach.take = size;
    this.getArticles();
  }
}
